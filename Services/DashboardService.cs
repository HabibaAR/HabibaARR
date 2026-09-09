using HabibaARR.Data;
using HabibaARR.Models;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(ApplicationDbContext context, ILogger<DashboardService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DashboardMetrics> GetMetricsAsync(string? userId = null)
    {
        var query = _context.Actions.AsQueryable();

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(a => a.ManagerId == userId || a.ResponsibleId == userId);
        }

        var totalActions = await query.CountAsync();
        var completedActions = await query.Where(a => a.Status == ActionStatus.Completed).CountAsync();

        var metrics = new DashboardMetrics
        {
            TotalActionPlans = await _context.ActionPlans.CountAsync(),
            TotalActions = totalActions,
            DraftActions = await query.Where(a => a.Status == ActionStatus.Draft).CountAsync(),
            NewActions = await query.Where(a => a.Status == ActionStatus.New).CountAsync(),
            AcceptedActions = await query.Where(a => a.Status == ActionStatus.Accepted).CountAsync(),
            RejectedActions = await query.Where(a => a.Status == ActionStatus.Rejected).CountAsync(),
            InProgressActions = await query.Where(a => a.Status == ActionStatus.InProgress).CountAsync(),
            SubmittedEvidenceActions = await query.Where(a => a.Status == ActionStatus.EvidenceSubmitted).CountAsync(),
            CompletedActions = completedActions,
            OverdueActions = await query.Where(a => a.DueDate < DateTime.Today && a.Status != ActionStatus.Completed).CountAsync(),
            CompletionRate = totalActions > 0 ? (completedActions * 100.0) / totalActions : 0
        };

        return metrics;
    }

    public async Task<Dictionary<string, int>> GetActionsByStatusAsync()
    {
        var result = new Dictionary<string, int>
        {
            { "Brouillon", await _context.Actions.Where(a => a.Status == ActionStatus.Draft).CountAsync() },
            { "Nouveau", await _context.Actions.Where(a => a.Status == ActionStatus.New).CountAsync() },
            { "Acceptée", await _context.Actions.Where(a => a.Status == ActionStatus.Accepted).CountAsync() },
            { "Rejetée", await _context.Actions.Where(a => a.Status == ActionStatus.Rejected).CountAsync() },
            { "En cours", await _context.Actions.Where(a => a.Status == ActionStatus.InProgress).CountAsync() },
            { "Preuve soumise", await _context.Actions.Where(a => a.Status == ActionStatus.EvidenceSubmitted).CountAsync() },
            { "Complétée", await _context.Actions.Where(a => a.Status == ActionStatus.Completed).CountAsync() }
        };

        return result;
    }

    public async Task<Dictionary<string, int>> GetActionsByResponsibleAsync(string? userId = null)
    {
        IQueryable<Action> query = _context.Actions;

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(a => a.ResponsibleId == userId);
        }

        var result = await query
            .GroupBy(a => a.Responsible!.FullName)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToDictionaryAsync(x => x.Name, x => x.Count);

        return result;
    }

    public async Task<Dictionary<DateTime, int>> GetActionCompletionTrendAsync(int days = 30)
    {
        var startDate = DateTime.UtcNow.AddDays(-days);

        var result = await _context.Actions
            .Where(a => a.CompletedAt.HasValue && a.CompletedAt >= startDate)
            .GroupBy(a => a.CompletedAt!.Value.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .OrderBy(x => x.Date)
            .ToDictionaryAsync(x => x.Date, x => x.Count);

        return result;
    }

    public async Task<IEnumerable<(string, int)>> GetOverdueActionsCountAsync()
    {
        var today = DateTime.Today;
        var result = await _context.Actions
            .Where(a => a.DueDate < today && a.Status != ActionStatus.Completed)
            .GroupBy(a => a.Responsible!.FullName)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();

        return result.Select(r => (r.Name, r.Count));
    }
}
