using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MIS.Infrastructure.Persistence.Reports.Geographic;

public class GeographicReportService
{
    private readonly ApplicationDbContext _context;

    public GeographicReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChartDataResponse> GetGeographicCoverageAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var totalWards = await _context.Set<Ward>()
            .Where(w => w.MunicipalityId == municipalityId)
            .CountAsync();

        var wardsWithHouses = await _context.Set<Ward>()
            .Where(w => w.MunicipalityId == municipalityId &&
                        w.Toles!.Any(t => _context.Set<House>()
                            .Any(h => h.ToleId == t.Id)))
            .CountAsync();

        wardsWithHouses = Math.Min(wardsWithHouses, totalWards);

        return new ChartDataResponse
        {
            ChartType = "doughnut",
            Title = $"Geographic Coverage - {municipality.NameEn}",
            Description = $"Wards with Data: {wardsWithHouses}/{totalWards}",
            Labels = new List<string> { "Covered", "Not Covered" },
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Wards",
                    Data = new List<int> { wardsWithHouses, totalWards - wardsWithHouses },
                    BackgroundColor = "#2ecc71,#e74c3c"
                }
            },
            TotalRecords = totalWards
        };
    }

    public async Task<ChartDataResponse> GetWardHierarchyAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var wardHierarchy = await _context.Set<Ward>()
            .Where(w => w.MunicipalityId == municipalityId)
            .Select(w => new
            {
                WardNumber = w.Number,
                ToleCount = w.Toles!.Count()
            })
            .OrderBy(x => x.WardNumber)
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Ward Hierarchy - {municipality.NameEn}",
            Labels = wardHierarchy.Select(x => $"Ward {x.WardNumber}").ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Toles",
                    Data = wardHierarchy.Select(x => (int)x.ToleCount).ToList(),
                    BackgroundColor = "#3498db"
                }
            },
            TotalRecords = wardHierarchy.Count
        };
    }
}
