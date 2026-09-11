namespace HabibaARR.Models;

public enum ActionLogEventType
{
    Created = 0,
    Submitted = 1,
    Accepted = 2,
    Rejected = 3,
    ProgressUpdated = 4,
    EvidenceSubmitted = 5,
    EvidenceApproved = 6,
    EvidenceRejected = 7,
    Completed = 8,
    Modified = 9,
    CommentAdded = 10
}

public class ActionLog
{
    public int Id { get; set; }
    public ActionLogEventType EventType { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign keys
    public int ActionId { get; set; }
    public string CreatedById { get; set; } = string.Empty;

    // Navigation properties
    public virtual Action Action { get; set; } = null!;
    public virtual ApplicationUser CreatedBy { get; set; } = null!;

    public string EventTypeDisplay => EventType switch
    {
        ActionLogEventType.Created => "Action créée",
        ActionLogEventType.Submitted => "Action transmise",
        ActionLogEventType.Accepted => "Action acceptée",
        ActionLogEventType.Rejected => "Action rejetée",
        ActionLogEventType.ProgressUpdated => "Progression mise à jour",
        ActionLogEventType.EvidenceSubmitted => "Preuve soumise",
        ActionLogEventType.EvidenceApproved => "Preuve validée",
        ActionLogEventType.EvidenceRejected => "Preuve rejetée",
        ActionLogEventType.Completed => "Action clôturée",
        ActionLogEventType.Modified => "Action modifiée",
        ActionLogEventType.CommentAdded => "Commentaire ajouté",
        _ => "Événement inconnu"
    };
}
