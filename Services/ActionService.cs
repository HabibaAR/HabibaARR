using HabibaARR.Data;
using HabibaARR.Models;
using Action = HabibaARR.Models.Action;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Services;

public class ActionService : IActionService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ActionService> _logger;

    public ActionService(ApplicationDbContext context, ILogger<ActionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Action?> GetByIdAsync(int id)
    {
        return await _context.Actions
            .Include(a => a.ActionPlan)
            .Include(a => a.Responsible)
            .Include(a => a.Manager)
            .Include(a => a.Logs)
            .Include(a => a.Evidences)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Action>> GetAllAsync()
    {
        return await _context.Actions
            .Include(a => a.ActionPlan)
            .Include(a => a.Responsible)
            .Include(a => a.Manager)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Action>> GetByPlanAsync(int planId)
    {
        return await _context.Actions
            .Where(a => a.ActionPlanId == planId)
            .Include(a => a.Responsible)
            .Include(a => a.Manager)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Action>> GetAssignedToAsync(string userId)
    {
        return await _context.Actions
            .Where(a => a.ResponsibleId == userId)
            .Include(a => a.ActionPlan)
            .Include(a => a.Manager)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<Action> CreateAsync(Action action, string userId)
    {
        action.ManagerId = userId;
        action.CreatedAt = DateTime.UtcNow;
        action.Status = ActionStatus.Draft;

        _context.Actions.Add(action);
        await _context.SaveChangesAsync();

        await AddLogAsync(action.Id, ActionLogEventType.Created, userId, "Action créée");

        _logger.LogInformation($"Action créée: {action.Reference}");
        return action;
    }

    public async Task<Action> UpdateAsync(Action action)
    {
        var existing = await GetByIdAsync(action.Id);
        if (existing == null)
            throw new InvalidOperationException("Action non trouvée");

        if (existing.Status != ActionStatus.Draft && existing.Status != ActionStatus.Rejected && existing.Status != ActionStatus.EvidenceRejected)
            throw new InvalidOperationException("Cette action ne peut pas être modifiée dans son état actuel");

        action.UpdatedAt = DateTime.UtcNow;
        _context.Actions.Update(action);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Action mise à jour: {action.Reference}");
        return action;
    }

    public async Task<Action> SubmitAsync(int id, string userId)
    {
        var action = await GetByIdAsync(id);
        if (action == null)
            throw new InvalidOperationException("Action non trouvée");

        action.Status = ActionStatus.New;
        action.UpdatedAt = DateTime.UtcNow;

        _context.Actions.Update(action);
        await _context.SaveChangesAsync();

        await AddLogAsync(id, ActionLogEventType.Submitted, userId, "Action transmise");

        _logger.LogInformation($"Action transmise: {action.Reference}");
        return action;
    }

    public async Task<Action> AcceptAsync(int id, string userId)
    {
        var action = await GetByIdAsync(id);
        if (action == null)
            throw new InvalidOperationException("Action non trouvée");

        if (action.Status != ActionStatus.New)
            throw new InvalidOperationException("Seules les actions nouvelles peuvent être acceptées");

        action.Status = ActionStatus.Accepted;
        action.UpdatedAt = DateTime.UtcNow;

        _context.Actions.Update(action);
        await _context.SaveChangesAsync();

        await AddLogAsync(id, ActionLogEventType.Accepted, userId, "Action acceptée");

        _logger.LogInformation($"Action acceptée: {action.Reference}");
        return action;
    }

    public async Task<Action> RejectAsync(int id, string userId, string reason)
    {
        var action = await GetByIdAsync(id);
        if (action == null)
            throw new InvalidOperationException("Action non trouvée");

        if (action.Status != ActionStatus.New)
            throw new InvalidOperationException("Seules les actions nouvelles peuvent être rejetées");

        action.Status = ActionStatus.Rejected;
        action.RejectionReason = reason;
        action.UpdatedAt = DateTime.UtcNow;

        _context.Actions.Update(action);
        await _context.SaveChangesAsync();

        await AddLogAsync(id, ActionLogEventType.Rejected, userId, $"Action rejetée. Raison: {reason}");

        _logger.LogInformation($"Action rejetée: {action.Reference}");
        return action;
    }

    public async Task<Action> CompleteAsync(int id, string userId)
    {
        var action = await GetByIdAsync(id);
        if (action == null)
            throw new InvalidOperationException("Action non trouvée");

        action.Status = ActionStatus.Completed;
        action.CompletedAt = DateTime.UtcNow;
        action.ProgressPercentage = 100;
        action.UpdatedAt = DateTime.UtcNow;

        _context.Actions.Update(action);
        await _context.SaveChangesAsync();

        await AddLogAsync(id, ActionLogEventType.Completed, userId, "Action clôturée");

        _logger.LogInformation($"Action clôturée: {action.Reference}");
        return action;
    }

    public async Task<IEnumerable<Action>> GetOverdueActionsAsync()
    {
        return await _context.Actions
            .Where(a => a.DueDate < DateTime.Today && a.Status != ActionStatus.Completed)
            .Include(a => a.Responsible)
            .Include(a => a.ActionPlan)
            .ToListAsync();
    }

    public async Task<int> GetCountByStatusAsync(ActionStatus status)
    {
        return await _context.Actions
            .Where(a => a.Status == status)
            .CountAsync();
    }

    public async Task<IEnumerable<Action>> GetByStatusAsync(ActionStatus status)
    {
        return await _context.Actions
            .Where(a => a.Status == status)
            .Include(a => a.Responsible)
            .Include(a => a.ActionPlan)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Actions.CountAsync();
    }

    public async Task AddLogAsync(int actionId, ActionLogEventType eventType, string userId, string comment = "", string oldValue = "", string newValue = "")
    {
        var log = new ActionLog
        {
            ActionId = actionId,
            EventType = eventType,
            CreatedById = userId,
            Comment = comment,
            OldValue = oldValue,
            NewValue = newValue,
            CreatedAt = DateTime.UtcNow
        };

        _context.ActionLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
