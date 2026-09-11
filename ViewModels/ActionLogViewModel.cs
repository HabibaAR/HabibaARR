using HabibaARR.Models;

namespace HabibaARR.ViewModels;

public class ActionLogViewModel
{
    public int Id { get; set; }
    public int ActionId { get; set; }
    public ActionLogEventType EventType { get; set; }
    public string EventTypeDisplay { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public string CreatedByRole { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
