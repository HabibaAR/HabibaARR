namespace HabibaARR.Models;

public enum ActionPlanStatus
{
    Draft = 0,
    Active = 1,
    Closed = 2,
    Archived = 3
}

public class ActionPlan
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ActionPlanStatus Status { get; set; } = ActionPlanStatus.Draft;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Foreign keys
    public string ManagerId { get; set; } = string.Empty;

    // Navigation properties
    public virtual ApplicationUser Manager { get; set; } = null!;
    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();
}
