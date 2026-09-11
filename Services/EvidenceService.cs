using HabibaARR.Data;
using HabibaARR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HabibaARR.Services;

public class EvidenceService : IEvidenceService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<EvidenceService> _logger;
    private readonly IActionService _actionService;

    public EvidenceService(
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<EvidenceService> logger,
        IActionService actionService)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
        _actionService = actionService;
    }

    public async Task<Evidence?> GetByIdAsync(int id)
    {
        return await _context.Evidences
            .Include(e => e.Action)
            .Include(e => e.SubmittedBy)
            .Include(e => e.ReviewedBy)
            .Include(e => e.Attachments)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Evidence>> GetByActionAsync(int actionId)
    {
        return await _context.Evidences
            .Where(e => e.ActionId == actionId)
            .Include(e => e.SubmittedBy)
            .Include(e => e.ReviewedBy)
            .Include(e => e.Attachments)
            .OrderByDescending(e => e.SubmittedAt)
            .ToListAsync();
    }

    public async Task<Evidence> SubmitAsync(Evidence evidence, string userId)
    {
        evidence.SubmittedById = userId;
        evidence.SubmittedAt = DateTime.UtcNow;
        evidence.Status = EvidenceStatus.Submitted;

        _context.Evidences.Add(evidence);
        await _context.SaveChangesAsync();

        // Update action status
        var action = await _context.Actions.FindAsync(evidence.ActionId);
        if (action != null)
        {
            action.Status = ActionStatus.EvidenceSubmitted;
            action.UpdatedAt = DateTime.UtcNow;
            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            await _actionService.AddLogAsync(
                evidence.ActionId,
                ActionLogEventType.EvidenceSubmitted,
                userId,
                "Preuve soumise");
        }

        _logger.LogInformation($"Preuve soumise pour l'action {evidence.ActionId}");
        return evidence;
    }

    public async Task<Evidence> ApproveAsync(int id, string reviewerId)
    {
        var evidence = await GetByIdAsync(id);
        if (evidence == null)
            throw new InvalidOperationException("Preuve non trouvée");

        evidence.Status = EvidenceStatus.Approved;
        evidence.ReviewedById = reviewerId;
        evidence.ApprovedAt = DateTime.UtcNow;

        _context.Evidences.Update(evidence);
        await _context.SaveChangesAsync();

        // Update action status
        var action = await _context.Actions.FindAsync(evidence.ActionId);
        if (action != null)
        {
            action.Status = ActionStatus.Completed;
            action.CompletedAt = DateTime.UtcNow;
            action.ProgressPercentage = 100;
            action.UpdatedAt = DateTime.UtcNow;
            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            await _actionService.AddLogAsync(
                evidence.ActionId,
                ActionLogEventType.EvidenceApproved,
                reviewerId,
                "Preuve validée");
        }

        _logger.LogInformation($"Preuve approuvée: {id}");
        return evidence;
    }

    public async Task<Evidence> RejectAsync(int id, string reviewerId, string reason)
    {
        var evidence = await GetByIdAsync(id);
        if (evidence == null)
            throw new InvalidOperationException("Preuve non trouvée");

        evidence.Status = EvidenceStatus.Rejected;
        evidence.ReviewedById = reviewerId;
        evidence.RejectedAt = DateTime.UtcNow;
        evidence.RejectionReason = reason;

        _context.Evidences.Update(evidence);
        await _context.SaveChangesAsync();

        // Update action status
        var action = await _context.Actions.FindAsync(evidence.ActionId);
        if (action != null)
        {
            action.Status = ActionStatus.EvidenceRejected;
            action.UpdatedAt = DateTime.UtcNow;
            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            await _actionService.AddLogAsync(
                evidence.ActionId,
                ActionLogEventType.EvidenceRejected,
                reviewerId,
                $"Preuve rejetée. Motif: {reason}");
        }

        _logger.LogInformation($"Preuve rejetée: {id}");
        return evidence;
    }

    public async Task DeleteAsync(int id)
    {
        var evidence = await GetByIdAsync(id);
        if (evidence == null)
            throw new InvalidOperationException("Preuve non trouvée");

        _context.Evidences.Remove(evidence);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Preuve supprimée: {id}");
    }

    public async Task<bool> UploadAttachmentAsync(int evidenceId, IFormFile file, string userId)
    {
        if (file == null || file.Length == 0)
            return false;

        var evidence = await GetByIdAsync(evidenceId);
        if (evidence == null)
            throw new InvalidOperationException("Preuve non trouvée");

        var maxSizeMB = _configuration.GetValue<int>("AppSettings:MaxUploadSizeMB");
        if (file.Length > maxSizeMB * 1024 * 1024)
            throw new InvalidOperationException($"Fichier trop volumineux. Maximum: {maxSizeMB}MB");

        var allowedExtensions = _configuration.GetSection("AppSettings:AllowedFileExtensions").Get<string[]>();
        var fileExtension = Path.GetExtension(file.FileName).TrimStart('.').ToLower();

        if (allowedExtensions != null && !allowedExtensions.Contains(fileExtension))
            throw new InvalidOperationException("Type de fichier non autorisé");

        var uploadsDir = Path.Combine("wwwroot", "uploads", "evidences");
        Directory.CreateDirectory(uploadsDir);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsDir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var attachment = new Attachment
        {
            EvidenceId = evidenceId,
            FileName = file.FileName,
            FileType = fileExtension,
            FileSizeBytes = file.Length,
            FilePath = $"/uploads/evidences/{fileName}",
            UploadedById = userId,
            UploadedAt = DateTime.UtcNow
        };

        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Fichier uploadé pour preuve {evidenceId}");
        return true;
    }

    public async Task DeleteAttachmentAsync(int attachmentId)
    {
        var attachment = await _context.Attachments.FindAsync(attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Pièce jointe non trouvée");

        var filePath = Path.Combine("wwwroot", attachment.FilePath.TrimStart('/'));
        if (File.Exists(filePath))
            File.Delete(filePath);

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Pièce jointe supprimée: {attachmentId}");
    }
}
