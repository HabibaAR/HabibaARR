using HabibaARR.Models;

namespace HabibaARR.Services;

public interface IActionService
{
    Task<Action?> GetByIdAsync(int id);
    Task<IEnumerable<Action>> GetByPlanAsync(int planId);
    Task<IEnumerable<Action>> GetAssignedToAsync(string userId);
    Task<Action> CreateAsync(Action action, string userId);
    Task<Action> UpdateAsync(Action action);
    Task<Action> SubmitAsync(int id, string userId);
    Task<Action> AcceptAsync(int id, string userId);
    Task<Action> RejectAsync(int id, string userId, string reason);
    Task<Action> CompleteAsync(int id, string userId);
    Task<IEnumerable<Action>> GetOverdueActionsAsync();
    Task<int> GetCountByStatusAsync(ActionStatus status);
    Task<IEnumerable<Action>> GetByStatusAsync(ActionStatus status);
    Task<int> GetTotalCountAsync();
    Task AddLogAsync(int actionId, ActionLogEventType eventType, string userId, string comment = "", string oldValue = "", string newValue = "");
}
