using HabibaARR.Services;

namespace HabibaARR.ViewModels;

public class DashboardViewModel
{
    public DashboardMetrics Metrics { get; set; } = new();
    public Dictionary<string, int> ActionsByStatus { get; set; } = new();
    public Dictionary<string, int> ActionsByResponsible { get; set; } = new();
    public IEnumerable<(string, int)> OverdueActions { get; set; } = new List<(string, int)>();
    public string UserRole { get; set; } = string.Empty;
}
