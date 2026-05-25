using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MIS.Infrastructure.Persistence.Reports.DataQuality;

public class DataQualityReportService
{
    private readonly ApplicationDbContext _context;

    public DataQualityReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChartDataResponse> GetDataQualityMetricsAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var totalHouses = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null &&
                        h.Tole.Ward.MunicipalityId == municipalityId)
            .CountAsync();

        var completeHouses = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null &&
                        h.Tole.Ward.MunicipalityId == municipalityId &&
                        !string.IsNullOrEmpty(h.HouseNumber) &&
                        !string.IsNullOrEmpty(h.Location))
            .CountAsync();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Data Quality Metrics - {municipality.NameEn}",
            Description = $"Completion Rate: {(totalHouses > 0 ? (decimal)(completeHouses * 100) / totalHouses : 0):F2}%",
            Labels = new List<string> { "Complete", "Incomplete" },
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Records",
                    Data = new List<int> { completeHouses, totalHouses - completeHouses },
                    BackgroundColor = "#2ecc71,#e74c3c"
                }
            },
            TotalRecords = totalHouses
        };
    }

    public async Task<ChartDataResponse> GetSubmissionStatusAsync(Guid municipalityId)
    {
        // Placeholder for submission status
        var statusData = new Dictionary<string, int>
        {
            { "Completed", 150 },
            { "In Progress", 45 },
            { "Rejected", 10 }
        };

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"Submission Status",
            Labels = statusData.Keys.ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = statusData.Values.ToList(),
                    BackgroundColor = "#2ecc71,#f39c12,#e74c3c"
                }
            },
            TotalRecords = statusData.Values.Sum()
        };
    }

    public async Task<ChartDataResponse> GetDataCompletenessAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Placeholder for data completeness by section
        var completenessData = new Dictionary<string, int>
        {
            { "Persons Data", 95 },
            { "House Data", 87 },
            { "Family Data", 92 },
            { "Education Data", 65 }
        };

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Data Completeness by Section - {municipality.NameEn}",
            Labels = completenessData.Keys.ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Completion %",
                    Data = completenessData.Values.ToList(),
                    BackgroundColor = "#3498db"
                }
            },
            TotalRecords = completenessData.Values.Count
        };
    }
}
