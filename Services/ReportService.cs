using ClosedXML.Excel;
using HabibaARR.Data;
using HabibaARR.Models;
using Action = HabibaARR.Models.Action;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReportService> _logger;

    public ReportService(ApplicationDbContext context, ILogger<ReportService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<byte[]> ExportActionsToExcelAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null)
    {
        var actions = await GetFilteredActionsAsync(statusFilter, priorityFilter, responsibleFilter);

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Actions");

            // Headers
            worksheet.Cell(1, 1).Value = "Référence";
            worksheet.Cell(1, 2).Value = "Intitulé";
            worksheet.Cell(1, 3).Value = "Plan";
            worksheet.Cell(1, 4).Value = "Responsable";
            worksheet.Cell(1, 5).Value = "Gestionnaire";
            worksheet.Cell(1, 6).Value = "Statut";
            worksheet.Cell(1, 7).Value = "Priorité";
            worksheet.Cell(1, 8).Value = "Progression %";
            worksheet.Cell(1, 9).Value = "Échéance";
            worksheet.Cell(1, 10).Value = "Créée le";
            worksheet.Cell(1, 11).Value = "Clôturée le";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 11);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data
            int row = 2;
            foreach (var action in actions.OrderByDescending(a => a.CreatedAt))
            {
                worksheet.Cell(row, 1).Value = action.Reference;
                worksheet.Cell(row, 2).Value = action.Title;
                worksheet.Cell(row, 3).Value = action.ActionPlan?.Title ?? "";
                worksheet.Cell(row, 4).Value = action.Responsible?.FullName ?? "";
                worksheet.Cell(row, 5).Value = action.Manager?.FullName ?? "";
                worksheet.Cell(row, 6).Value = GetStatusDisplay(action.Status);
                worksheet.Cell(row, 7).Value = GetPriorityDisplay(action.Priority);
                worksheet.Cell(row, 8).Value = action.ProgressPercentage;
                worksheet.Cell(row, 9).Value = action.DueDate.ToShortDateString();
                worksheet.Cell(row, 10).Value = action.CreatedAt.ToString("g");
                worksheet.Cell(row, 11).Value = action.CompletedAt?.ToString("g") ?? "";

                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    public async Task<byte[]> ExportActionsToPdfAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null)
    {
        var actions = await GetFilteredActionsAsync(statusFilter, priorityFilter, responsibleFilter);

        // For PDF, we'll create a simple HTML that can be converted
        // In production, use a library like SelectPdf or Syncfusion
        var htmlContent = GeneratePdfHtml(actions);

        // For now, return a placeholder
        // In production, use SelectPdf.HtmlToImage or similar
        return System.Text.Encoding.UTF8.GetBytes(htmlContent);
    }

    public async Task<byte[]> ExportActionPlansToExcelAsync()
    {
        var plans = await _context.ActionPlans
            .Include(ap => ap.Manager)
            .Include(ap => ap.Actions)
            .OrderByDescending(ap => ap.CreatedAt)
            .ToListAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Plans d'Action");

            // Headers
            worksheet.Cell(1, 1).Value = "Référence";
            worksheet.Cell(1, 2).Value = "Intitulé";
            worksheet.Cell(1, 3).Value = "Gestionnaire";
            worksheet.Cell(1, 4).Value = "Statut";
            worksheet.Cell(1, 5).Value = "Nb Actions";
            worksheet.Cell(1, 6).Value = "Date Début";
            worksheet.Cell(1, 7).Value = "Date Fin";
            worksheet.Cell(1, 8).Value = "Créé le";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 8);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data
            int row = 2;
            foreach (var plan in plans)
            {
                worksheet.Cell(row, 1).Value = plan.Reference;
                worksheet.Cell(row, 2).Value = plan.Title;
                worksheet.Cell(row, 3).Value = plan.Manager?.FullName ?? "";
                worksheet.Cell(row, 4).Value = plan.Status.ToString();
                worksheet.Cell(row, 5).Value = plan.Actions.Count;
                worksheet.Cell(row, 6).Value = plan.StartDate.ToShortDateString();
                worksheet.Cell(row, 7).Value = plan.EndDate.ToShortDateString();
                worksheet.Cell(row, 8).Value = plan.CreatedAt.ToString("g");

                row++;
            }

            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    public async Task<IEnumerable<Action>> GetFilteredActionsAsync(
        string? statusFilter = null,
        string? priorityFilter = null,
        string? responsibleFilter = null)
    {
        var query = _context.Actions
            .Include(a => a.ActionPlan)
            .Include(a => a.Responsible)
            .Include(a => a.Manager)
            .AsQueryable();

        if (!string.IsNullOrEmpty(statusFilter) && Enum.TryParse<ActionStatus>(statusFilter, out var status))
        {
            query = query.Where(a => a.Status == status);
        }

        if (!string.IsNullOrEmpty(priorityFilter) && Enum.TryParse<ActionPriority>(priorityFilter, out var priority))
        {
            query = query.Where(a => a.Priority == priority);
        }

        if (!string.IsNullOrEmpty(responsibleFilter))
        {
            query = query.Where(a => a.ResponsibleId == responsibleFilter);
        }

        return await query.ToListAsync();
    }

    private string GetStatusDisplay(ActionStatus status)
    {
        return status switch
        {
            ActionStatus.Draft => "Brouillon",
            ActionStatus.New => "Nouveau",
            ActionStatus.Accepted => "Acceptée",
            ActionStatus.Rejected => "Rejetée",
            ActionStatus.InProgress => "En cours",
            ActionStatus.EvidenceSubmitted => "Preuve soumise",
            ActionStatus.EvidenceRejected => "Preuve rejetée",
            ActionStatus.Completed => "Complétée",
            _ => "Inconnu"
        };
    }

    private string GetPriorityDisplay(ActionPriority priority)
    {
        return priority switch
        {
            ActionPriority.Low => "Basse",
            ActionPriority.Medium => "Moyenne",
            ActionPriority.High => "Haute",
            ActionPriority.Critical => "Critique",
            _ => "Inconnu"
        };
    }

    private string GeneratePdfHtml(IEnumerable<Action> actions)
    {
        var html = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>Rapport des Actions</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        h1 { color: #333; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; font-weight: bold; }
        tr:nth-child(even) { background-color: #f9f9f9; }
    </style>
</head>
<body>
    <h1>Rapport des Actions</h1>
    <p>Date de génération: " + DateTime.Now.ToString("g") + @"</p>
    <table>
        <thead>
            <tr>
                <th>Référence</th>
                <th>Intitulé</th>
                <th>Responsable</th>
                <th>Statut</th>
                <th>Progression</th>
                <th>Échéance</th>
            </tr>
        </thead>
        <tbody>";

        foreach (var action in actions)
        {
            html += $@"
            <tr>
                <td>{action.Reference}</td>
                <td>{action.Title}</td>
                <td>{action.Responsible?.FullName}</td>
                <td>{GetStatusDisplay(action.Status)}</td>
                <td>{action.ProgressPercentage}%</td>
                <td>{action.DueDate:d}</td>
            </tr>";
        }

        html += @"
        </tbody>
    </table>
</body>
</html>";

        return html;
    }
}
