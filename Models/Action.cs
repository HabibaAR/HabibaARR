namespace HabibaARR.Models;

public enum ActionStatus
{
    Draft = 0,
    New = 1,
    Accepted = 2,
    Rejected = 3,
    InProgress = 4,
    EvidenceSubmitted = 5,
    EvidenceRejected = 6,
    Completed = 7
}

public enum ActionPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

public class Action
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActionStatus Status { get; set; } = ActionStatus.Draft;
    public ActionPriority Priority { get; set; } = ActionPriority.Medium;

    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public int ProgressPercentage { get; set; } = 0;
    public string RejectionReason { get; set; } = string.Empty;

    // Foreign keys
    public int ActionPlanId { get; set; }
    public string ResponsibleId { get; set; } = string.Empty;
    public string ManagerId { get; set; } = string.Empty;

    // Navigation properties
    public virtual ActionPlan ActionPlan { get; set; } = null!;
    public virtual ApplicationUser Responsible { get; set; } = null!;
    public virtual ApplicationUser Manager { get; set; } = null!;
    public virtual ICollection<ActionLog> Logs { get; set; } = new List<ActionLog>();
    public virtual ICollection<Evidence> Evidences { get; set; } = new List<Evidence>();

    public bool IsOverdue => DueDate < DateTime.Today && Status != ActionStatus.Completed;
}
