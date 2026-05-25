using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MIS.Infrastructure.Persistence.Reports.FamilyReports;

public class FamilyReportService
{
    private readonly ApplicationDbContext _context;

    public FamilyReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChartDataResponse> GetFamiliesByEthnicityAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var familiesByEthnicity = await _context.Set<Family>()
            .Where(f => f.House != null && f.House.Tole != null && f.House.Tole.Ward != null &&
                        f.House.Tole.Ward.MunicipalityId == municipalityId)
            .GroupBy(f => f.Ethnicity != null ? f.Ethnicity.LabelEn : "Unknown")
            .Select(g => new
            {
                Ethnicity = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Families by Ethnicity - {municipality.NameEn}",
            Labels = familiesByEthnicity.Select(x => x.Ethnicity).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Number of Families",
                    Data = familiesByEthnicity.Select(x => x.Count).ToList(),
                    BackgroundColor = "#e74c3c"
                }
            },
            TotalRecords = familiesByEthnicity.Sum(x => x.Count)
        };
    }

    public async Task<ChartDataResponse> GetFamiliesByReligionAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        var familiesByReligion = await _context.Set<Family>()
            .Where(f => f.House != null && f.House.Tole != null && f.House.Tole.Ward != null &&
                        f.House.Tole.Ward.MunicipalityId == municipalityId)
            .GroupBy(f => f.Religion != null ? f.Religion.LabelEn : "Unknown")
            .Select(g => new
            {
                Religion = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"Families by Religion - {municipality.NameEn}",
            Labels = familiesByReligion.Select(x => x.Religion).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = familiesByReligion.Select(x => x.Count).ToList()
                }
            },
            TotalRecords = familiesByReligion.Sum(x => x.Count)
        };
    }
}
