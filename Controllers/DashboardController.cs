using HabibaARR.Services;
using HabibaARR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabibaARR.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;
    private readonly IUserService _userService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(
        IDashboardService dashboardService,
        IUserService userService,
        ILogger<DashboardController> logger)
    {
        _dashboardService = dashboardService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = await _userService.GetUserRoleAsync(userId!);

            var metrics = await _dashboardService.GetMetricsAsync(userId);
            var actionsByStatus = await _dashboardService.GetActionsByStatusAsync();
            var actionsByResponsible = await _dashboardService.GetActionsByResponsibleAsync();
            var overdueActions = await _dashboardService.GetOverdueActionsCountAsync();

            var model = new DashboardViewModel
            {
                Metrics = metrics,
                ActionsByStatus = actionsByStatus,
                ActionsByResponsible = actionsByResponsible,
                OverdueActions = overdueActions,
                UserRole = userRole
            };

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement du dashboard");
            return View(new DashboardViewModel());
        }
    }
}
