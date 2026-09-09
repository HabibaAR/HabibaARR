using HabibaARR.Models;

namespace HabibaARR.Services;

public interface IEvidenceService
{
    Task<Evidence?> GetByIdAsync(int id);
    Task<IEnumerable<Evidence>> GetByActionAsync(int actionId);
    Task<Evidence> SubmitAsync(Evidence evidence, string userId);
    Task<Evidence> ApproveAsync(int id, string reviewerId);
    Task<Evidence> RejectAsync(int id, string reviewerId, string reason);
    Task DeleteAsync(int id);
    Task<bool> UploadAttachmentAsync(int evidenceId, IFormFile file, string userId);
    Task DeleteAttachmentAsync(int attachmentId);
}
