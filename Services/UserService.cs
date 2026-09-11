using HabibaARR.Data;
using HabibaARR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger<UserService> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<ApplicationUser?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IEnumerable<ApplicationUser>> GetByRoleAsync(string role)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(role);
        return usersInRole.Where(u => u.IsActive).ToList();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    public async Task<bool> IsManagerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await _userManager.IsInRoleAsync(user, "ADMIN")
            || await _userManager.IsInRoleAsync(user, "DIRECTEUR")
            || await _userManager.IsInRoleAsync(user, "GESTIONNAIRE");
    }

    public async Task<bool> IsResponsibleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        return await _userManager.IsInRoleAsync(user, "RESPONSABLE");
    }

    public async Task<string> GetUserRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return "UNKNOWN";

        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault() ?? "UNKNOWN";
    }
}
