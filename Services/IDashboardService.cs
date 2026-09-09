namespace HabibaARR.Services;

public class DashboardMetrics
{
    public int TotalActionPlans { get; set; }
    public int TotalActions { get; set; }
    public int DraftActions { get; set; }
    public int NewActions { get; set; }
    public int AcceptedActions { get; set; }
    public int RejectedActions { get; set; }
    public int InProgressActions { get; set; }
    public int SubmittedEvidenceActions { get; set; }
    public int CompletedActions { get; set; }
    public int OverdueActions { get; set; }
    public double CompletionRate { get; set; }
}

public interface IDashboardService
{
    Task<DashboardMetrics> GetMetricsAsync(string? userId = null);
    Task<Dictionary<string, int>> GetActionsByStatusAsync();
    Task<Dictionary<string, int>> GetActionsByResponsibleAsync(string? userId = null);
    Task<Dictionary<DateTime, int>> GetActionCompletionTrendAsync(int days = 30);
    Task<IEnumerable<(string, int)>> GetOverdueActionsCountAsync();
}
