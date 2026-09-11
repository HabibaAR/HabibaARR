using Microsoft.AspNetCore.Identity;

namespace HabibaARR.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;

    public string FullName => $"{FirstName} {LastName}";

    // Navigation properties
    public virtual ICollection<ActionPlan> CreatedActionPlans { get; set; } = new List<ActionPlan>();
    public virtual ICollection<Action> AssignedActions { get; set; } = new List<Action>();
    public virtual ICollection<ActionLog> ActionLogs { get; set; } = new List<ActionLog>();
}
