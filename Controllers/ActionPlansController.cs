using HabibaARR.Models;
using HabibaARR.Services;
using HabibaARR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabibaARR.Controllers;

[Authorize]
public class ActionPlansController : Controller
{
    private readonly IActionPlanService _actionPlanService;
    private readonly IActionService _actionService;
    private readonly IUserService _userService;
    private readonly ILogger<ActionPlansController> _logger;

    public ActionPlansController(
        IActionPlanService actionPlanService,
        IActionService actionService,
        IUserService userService,
        ILogger<ActionPlansController> logger)
    {
        _actionPlanService = actionPlanService;
        _actionService = actionService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        var isManager = await _userService.IsManagerAsync(userId);

        if (!isManager)
        {
            return Forbid();
        }

        var plans = await _actionPlanService.GetAllAsync(userId);
        var viewModels = plans.Select(p => new ActionPlanViewModel
        {
            Id = p.Id,
            Reference = p.Reference,
            Title = p.Title,
            Description = p.Description,
            Status = p.Status,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            ManagerId = p.ManagerId,
            ManagerName = p.Manager?.FullName ?? "",
            ActionCount = p.Actions.Count,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await _actionPlanService.GetByIdAsync(id);
        if (plan == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (plan.ManagerId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        var model = new ActionPlanViewModel
        {
            Id = plan.Id,
            Reference = plan.Reference,
            Title = plan.Title,
            Description = plan.Description,
            Status = plan.Status,
            StartDate = plan.StartDate,
            EndDate = plan.EndDate,
            ManagerId = plan.ManagerId,
            ManagerName = plan.Manager?.FullName ?? "",
            ActionCount = plan.Actions.Count,
            CreatedAt = plan.CreatedAt,
            UpdatedAt = plan.UpdatedAt
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActionPlanViewModel model)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        var isManager = await _userService.IsManagerAsync(userId);

        if (!isManager)
            return Forbid();

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var plan = new ActionPlan
            {
                Reference = model.Reference,
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ManagerId = userId
            };

            var createdPlan = await _actionPlanService.CreateAsync(plan, userId);
            return RedirectToAction(nameof(Details), new { id = createdPlan.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du plan");
            ModelState.AddModelError("", "Erreur lors de la création du plan");
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Submit(int id)
    {
        var plan = await _actionPlanService.GetByIdAsync(id);
        if (plan == null)
            return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
        if (plan.ManagerId != userId && !User.IsInRole("ADMIN"))
            return Forbid();

        try
        {
            await _actionPlanService.SubmitAsync(id);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la transmission du plan");
            return BadRequest();
        }
    }
}
