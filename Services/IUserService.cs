using HabibaARR.Models;

namespace HabibaARR.Services;

public interface IUserService
{
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<IEnumerable<ApplicationUser>> GetByRoleAsync(string role);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task<bool> IsManagerAsync(string userId);
    Task<bool> IsResponsibleAsync(string userId);
    Task<string> GetUserRoleAsync(string userId);
}
