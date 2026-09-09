using HabibaARR.Models;

namespace HabibaARR.ViewModels;

public class EvidenceViewModel
{
    public int Id { get; set; }
    public int ActionId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public EvidenceStatus Status { get; set; }
    public string SubmittedByName { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public string? ReviewedByName { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string RejectionReason { get; set; } = string.Empty;
    public DateTime? RejectedAt { get; set; }
    public IEnumerable<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
}

public class CreateEvidenceViewModel
{
    public int ActionId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public IFormFile? AttachmentFile { get; set; }
}

public class AttachmentViewModel
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string FileSizeDisplay { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string UploadedByName { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
