using HabibaARR.Models;

namespace HabibaARR.Services;

public interface IReportService
{
    Task<byte[]> ExportActionsToExcelAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null);

    Task<byte[]> ExportActionsToPdfAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null);

    Task<byte[]> ExportActionPlansToExcelAsync();
    Task<IEnumerable<Action>> GetFilteredActionsAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null);
}
