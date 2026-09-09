namespace HabibaARR.Models;

public enum EvidenceStatus
{
    Submitted = 0,
    Approved = 1,
    Rejected = 2
}

public class Evidence
{
    public int Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public EvidenceStatus Status { get; set; } = EvidenceStatus.Submitted;
    public string RejectionReason { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedAt { get; set; }
    public DateTime? RejectedAt { get; set; }

    // Foreign keys
    public int ActionId { get; set; }
    public string SubmittedById { get; set; } = string.Empty;
    public string? ReviewedById { get; set; }

    // Navigation properties
    public virtual Action Action { get; set; } = null!;
    public virtual ApplicationUser SubmittedBy { get; set; } = null!;
    public virtual ApplicationUser? ReviewedBy { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}
