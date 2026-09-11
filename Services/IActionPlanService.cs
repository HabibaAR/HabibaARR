using HabibaARR.Models;

namespace HabibaARR.Services;

public interface IActionPlanService
{
    Task<ActionPlan?> GetByIdAsync(int id);
    Task<IEnumerable<ActionPlan>> GetAllAsync(string userId);
    Task<ActionPlan> CreateAsync(ActionPlan plan, string userId);
    Task<ActionPlan> UpdateAsync(ActionPlan plan);
    Task DeleteAsync(int id);
    Task<ActionPlan> SubmitAsync(int id);
    Task<IEnumerable<ActionPlan>> GetByStatusAsync(ActionPlanStatus status, string userId);
    Task<int> GetTotalCountAsync(string userId);
    Task<int> GetActiveCountAsync(string userId);
}
