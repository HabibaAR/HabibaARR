using HabibaARR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabibaARR.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IActionService _actionService;
    private readonly IActionPlanService _actionPlanService;
    private readonly IReportService _reportService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(
        IActionService actionService,
        IActionPlanService actionPlanService,
        IReportService reportService,
        ILogger<ReportsController> logger)
    {
        _actionService = actionService;
        _actionPlanService = actionPlanService;
        _reportService = reportService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "";
        var actionPlans = await _actionPlanService.GetAllAsync(userId);
        var viewModel = new ReportsIndexViewModel
        {
            ActionPlans = actionPlans.Select(ap => new ActionPlanSummary
            {
                Id = ap.Id,
                Reference = ap.Reference,
                Title = ap.Title,
                Status = ap.Status.ToString()
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ExportActionsToExcel()
    {
        try
        {
            var excelFile = await _reportService.ExportActionsToExcelAsync();
            var fileName = $"Actions_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            _logger.LogInformation($"Export actions par {User.Identity?.Name}");
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur export Excel");
            TempData["Error"] = "Erreur lors de l'export.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> ExportPlansToExcel()
    {
        try
        {
            var excelFile = await _reportService.ExportActionPlansToExcelAsync();
            var fileName = $"Plans_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            _logger.LogInformation($"Export plans par {User.Identity?.Name}");
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur export plans");
            TempData["Error"] = "Erreur lors de l'export.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> ExportDashboardSnapshot()
    {
        try
        {
            var excelFile = await _reportService.ExportActionsToExcelAsync();
            var fileName = $"Dashboard_Snapshot_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            _logger.LogInformation($"Export snapshot par {User.Identity?.Name}");
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur export snapshot");
            TempData["Error"] = "Erreur lors de l'export.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> ExportActionsToPdf()
    {
        try
        {
            var pdfFile = await _reportService.ExportActionsToPdfAsync();
            var fileName = $"Actions_Export_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            _logger.LogInformation($"Export actions PDF par {User.Identity?.Name}");
            return File(pdfFile, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur export PDF");
            TempData["Error"] = "Erreur lors de l'export PDF.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> ExportPlansToPdf()
    {
        try
        {
            var actions = await _actionService.GetAllAsync();
            var pdfFile = await _reportService.ExportActionsToPdfAsync();
            var fileName = $"Plans_Export_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            _logger.LogInformation($"Export plans PDF par {User.Identity?.Name}");
            return File(pdfFile, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur export plans PDF");
            TempData["Error"] = "Erreur lors de l'export PDF.";
            return RedirectToAction(nameof(Index));
        }
    }
}

public class ReportsIndexViewModel
{
    public List<ActionPlanSummary> ActionPlans { get; set; } = new();
    public DateTime ExportDate { get; set; } = DateTime.Now;
}

public class ActionPlanSummary
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
