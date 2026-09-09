using HabibaARR.Data;
using HabibaARR.Models;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Services;

public class ActionPlanService : IActionPlanService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ActionPlanService> _logger;

    public ActionPlanService(ApplicationDbContext context, ILogger<ActionPlanService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ActionPlan?> GetByIdAsync(int id)
    {
        return await _context.ActionPlans
            .Include(ap => ap.Manager)
            .Include(ap => ap.Actions)
            .FirstOrDefaultAsync(ap => ap.Id == id);
    }

    public async Task<IEnumerable<ActionPlan>> GetAllAsync(string userId)
    {
        return await _context.ActionPlans
            .Where(ap => ap.ManagerId == userId)
            .Include(ap => ap.Manager)
            .Include(ap => ap.Actions)
            .OrderByDescending(ap => ap.CreatedAt)
            .ToListAsync();
    }

    public async Task<ActionPlan> CreateAsync(ActionPlan plan, string userId)
    {
        plan.ManagerId = userId;
        plan.CreatedAt = DateTime.UtcNow;
        plan.Status = ActionPlanStatus.Draft;

        _context.ActionPlans.Add(plan);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Plan d'action créé: {plan.Reference} par l'utilisateur {userId}");
        return plan;
    }

    public async Task<ActionPlan> UpdateAsync(ActionPlan plan)
    {
        plan.UpdatedAt = DateTime.UtcNow;
        _context.ActionPlans.Update(plan);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Plan d'action mis à jour: {plan.Reference}");
        return plan;
    }

    public async Task DeleteAsync(int id)
    {
        var plan = await GetByIdAsync(id);
        if (plan == null)
            throw new InvalidOperationException("Plan d'action non trouvé");

        if (plan.Status != ActionPlanStatus.Draft)
            throw new InvalidOperationException("Seuls les brouillons peuvent être supprimés");

        _context.ActionPlans.Remove(plan);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Plan d'action supprimé: {plan.Reference}");
    }

    public async Task<ActionPlan> SubmitAsync(int id)
    {
        var plan = await GetByIdAsync(id);
        if (plan == null)
            throw new InvalidOperationException("Plan d'action non trouvé");

        plan.Status = ActionPlanStatus.Active;
        plan.UpdatedAt = DateTime.UtcNow;

        _context.ActionPlans.Update(plan);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Plan d'action transmis: {plan.Reference}");
        return plan;
    }

    public async Task<IEnumerable<ActionPlan>> GetByStatusAsync(ActionPlanStatus status, string userId)
    {
        return await _context.ActionPlans
            .Where(ap => ap.Status == status && ap.ManagerId == userId)
            .Include(ap => ap.Actions)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string userId)
    {
        return await _context.ActionPlans
            .Where(ap => ap.ManagerId == userId)
            .CountAsync();
    }

    public async Task<int> GetActiveCountAsync(string userId)
    {
        return await _context.ActionPlans
            .Where(ap => ap.ManagerId == userId && ap.Status == ActionPlanStatus.Active)
            .CountAsync();
    }
}
