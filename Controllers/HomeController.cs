using HabibaARR.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HabibaARR.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated ?? false)
            return RedirectToAction(nameof(DashboardController.Index), "Dashboard");

        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
