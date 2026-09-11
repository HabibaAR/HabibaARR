using HabibaARR.Models;

namespace HabibaARR.ViewModels;

public class ActionPlanViewModel
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActionPlanStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ManagerId { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public int ActionCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateActionPlanViewModel
{
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(3);
}

public class EditActionPlanViewModel : CreateActionPlanViewModel
{
    public int Id { get; set; }
}
