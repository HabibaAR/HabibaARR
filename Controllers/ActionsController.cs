using HabibaARR.Models;
using Action = HabibaARR.Models.Action;
using HabibaARR.Services;
using HabibaARR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabibaARR.Controllers;

[Authorize]
public class ActionsController : Controller
{
    private readonly IActionService _actionService;
    private readonly IActionPlanService _actionPlanService;
    private readonly IEvidenceService _evidenceService;
    private readonly IUserService _userService;
    private readonly ILogger<ActionsController> _logger;

    public ActionsController(
        IActionService actionService,
        IActionPlanService actionPlanService,
        IEvidenceService evidenceService,
        IUserService userService,
        ILogger<ActionsController> logger)
    {
        _actionService = actionService;
        _actionPlanService = actionPlanService;
        _evidenceService = evidenceService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int? planId = null)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

        IEnumerable<Action> actions;
        if (planId.HasValue)
        {
            actions = await _actionService.GetByPlanAsync(planId.Value);
        }
        else if (User.IsInRole("ADMIN") || User.IsInRole("DIRECTEUR"))
        {
            actions = await _actionService.GetAllAsync();
        }
        else if (User.IsInRole("GESTIONNAIRE"))
        {
            actions = await _actionService.GetAllAsync();
        }
        else if (await _userService.IsResponsibleAsync(userId))
        {
            actions = await _actionService.GetAssignedToAsync(userId);
        }
        else
        {
            return Forbid();
        }

        var viewModels = actions.Select(MapToViewModel).ToList();
        ViewData["PlanId"] = planId;

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var action = await _actionService.GetByIdAsync(id);
        if (action == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (action.ResponsibleId != userId && action.ManagerId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        var logs = action.Logs
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new ActionLogViewModel
            {
                Id = l.Id,
                ActionId = l.ActionId,
                EventType = l.EventType,
                EventTypeDisplay = l.EventTypeDisplay,
                Comment = l.Comment,
                OldValue = l.OldValue,
                NewValue = l.NewValue,
                CreatedByName = l.CreatedBy?.FullName ?? "",
                CreatedAt = l.CreatedAt
            }).ToList();

        var evidences = action.Evidences
            .OrderByDescending(e => e.SubmittedAt)
            .Select(e => new EvidenceViewModel
            {
                Id = e.Id,
                ActionId = e.ActionId,
                Comment = e.Comment,
                Status = e.Status,
                SubmittedByName = e.SubmittedBy?.FullName ?? "",
                SubmittedAt = e.SubmittedAt,
                ReviewedByName = e.ReviewedBy?.FullName,
                ApprovedAt = e.ApprovedAt,
                RejectionReason = e.RejectionReason,
                RejectedAt = e.RejectedAt,
                Attachments = e.Attachments.Select(a => new AttachmentViewModel
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FileType = a.FileType,
                    FileSizeDisplay = a.FileSizeDisplay,
                    FilePath = a.FilePath,
                    UploadedByName = a.UploadedBy?.FullName ?? "",
                    UploadedAt = a.UploadedAt
                }).ToList()
            }).ToList();

        var model = new ActionDetailViewModel
        {
            Action = MapToViewModel(action),
            Logs = logs,
            Evidences = evidences,
            RejectionReason = action.RejectionReason
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int planId)
    {
        var plan = await _actionPlanService.GetByIdAsync(planId);
        if (plan == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (plan.ManagerId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        var model = new CreateActionViewModel { ActionPlanId = planId };
        ViewData["Responsibles"] = await _userService.GetByRoleAsync("RESPONSABLE");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Responsibles"] = await _userService.GetByRoleAsync("RESPONSABLE");
            return View(model);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        var isManager = await _userService.IsManagerAsync(userId);

        if (!isManager)
            return Forbid();

        try
        {
            var action = new Action
            {
                Reference = model.Reference,
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                StartDate = model.StartDate,
                DueDate = model.DueDate,
                ActionPlanId = model.ActionPlanId,
                ResponsibleId = model.ResponsibleId,
                ManagerId = userId
            };

            var createdAction = await _actionService.CreateAsync(action, userId);
            return RedirectToAction(nameof(Details), new { id = createdAction.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création de l'action");
            ModelState.AddModelError("", "Erreur lors de la création de l'action");
            ViewData["Responsibles"] = await _userService.GetByRoleAsync("RESPONSABLE");
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Accept(int id)
    {
        var action = await _actionService.GetByIdAsync(id);
        if (action == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (action.ResponsibleId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        try
        {
            await _actionService.AcceptAsync(id, userId);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'acceptation de l'action");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id, string reason)
    {
        var action = await _actionService.GetByIdAsync(id);
        if (action == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (action.ResponsibleId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        try
        {
            await _actionService.RejectAsync(id, userId, reason);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du rejet de l'action");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> SubmitEvidence(int id, string comment, IFormFileCollection attachments)
    {
        var action = await _actionService.GetByIdAsync(id);
        if (action == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (action.ResponsibleId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        try
        {
            var evidence = new Evidence
            {
                ActionId = id,
                Comment = comment,
                Status = EvidenceStatus.Submitted,
                SubmittedById = userId,
                SubmittedAt = DateTime.UtcNow
            };

            var submittedEvidence = await _evidenceService.SubmitAsync(evidence, userId);

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        try
                        {
                            await _evidenceService.UploadAttachmentAsync(submittedEvidence.Id, file, userId);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Erreur lors de l'upload du fichier {file.FileName}");
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la soumission de la preuve");
            TempData["Error"] = "Erreur lors de la soumission de la preuve";
            return RedirectToAction(nameof(Details), new { id });
        }
    }

    private ActionViewModel MapToViewModel(Action action)
    {
        return new ActionViewModel
        {
            Id = action.Id,
            Reference = action.Reference,
            Title = action.Title,
            Description = action.Description,
            Status = action.Status,
            Priority = action.Priority,
            StartDate = action.StartDate,
            DueDate = action.DueDate,
            ProgressPercentage = action.ProgressPercentage,
            ResponsibleId = action.ResponsibleId,
            ResponsibleName = action.Responsible?.FullName ?? "",
            ManagerId = action.ManagerId,
            ManagerName = action.Manager?.FullName ?? "",
            ActionPlanId = action.ActionPlanId,
            ActionPlanTitle = action.ActionPlan?.Title ?? "",
            IsOverdue = action.IsOverdue,
            CreatedAt = action.CreatedAt,
            CompletedAt = action.CompletedAt
        };
    }
}
