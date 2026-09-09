using HabibaARR.Models;

namespace HabibaARR.ViewModels;

public class ActionViewModel
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActionStatus Status { get; set; }
    public ActionPriority Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public int ProgressPercentage { get; set; }
    public string ResponsibleId { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string ManagerId { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public int ActionPlanId { get; set; }
    public string ActionPlanTitle { get; set; } = string.Empty;
    public bool IsOverdue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class CreateActionViewModel
{
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActionPriority Priority { get; set; } = ActionPriority.Medium;
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; } = DateTime.Today.AddMonths(1);
    public string ResponsibleId { get; set; } = string.Empty;
    public int ActionPlanId { get; set; }
}

public class EditActionViewModel : CreateActionViewModel
{
    public int Id { get; set; }
    public ActionStatus Status { get; set; }
    public int ProgressPercentage { get; set; }
}

public class ActionDetailViewModel
{
    public ActionViewModel Action { get; set; } = new();
    public IEnumerable<ActionLogViewModel> Logs { get; set; } = new List<ActionLogViewModel>();
    public IEnumerable<EvidenceViewModel> Evidences { get; set; } = new List<EvidenceViewModel>();
    public string RejectionReason { get; set; } = string.Empty;
}
