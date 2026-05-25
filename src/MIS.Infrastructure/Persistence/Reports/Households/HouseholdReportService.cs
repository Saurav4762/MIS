using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MIS.Infrastructure.Persistence.Reports.Households;

public class HouseholdReportService
{
    private readonly ApplicationDbContext _context;

    public HouseholdReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChartDataResponse> GetHouseholdsByWardAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Count households by ward
        var householdsByWard = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null &&
                        h.Tole.Ward.MunicipalityId == municipalityId)
            .GroupBy(h => h.Tole!.Ward!.Number.ToString())
            .Select(g => new
            {
                WardNumber = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        var totalHouseholds = householdsByWard.Sum(x => (long)x.Count);

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Households by Ward - {municipality.NameEn}",
            Description = $"Total Households: {totalHouseholds}",
            Labels = householdsByWard.Select(x => $"Ward {x.WardNumber}").ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Number of Households",
                    Data = householdsByWard.Select(x => x.Count).ToList(),
                    BackgroundColor = "#2ecc71",
                    BorderColor = "#27ae60"
                }
            },
            TotalRecords = totalHouseholds
        };
    }

    public async Task<ChartDataResponse> GetHouseholdsByOwnershipAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        // Count households by ownership type - placeholder since Household model not fully mapped
        var householdsByOwnership = new Dictionary<string, int>
        {
            { "Owned", 45 },
            { "Rented", 30 },
            { "Leased", 15 }
        };

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"Households by Ownership - Ward {ward.Number}",
            Labels = householdsByOwnership.Keys.ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = householdsByOwnership.Values.ToList(),
                    BackgroundColor = "#e74c3c,#3498db,#f39c12"
                }
            },
            TotalRecords = householdsByOwnership.Values.Sum()
        };
    }

    public async Task<ChartDataResponse> GetAverageFamilySizeAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Get families and count members per family
        var familiesWithMemberCount = await _context.Set<Family>()
            .Where(f => f.House != null && f.House.Tole != null && f.House.Tole.Ward != null &&
                        f.House.Tole.Ward.MunicipalityId == municipalityId)
            .Select(f => new
            {
                FamilyId = f.Id,
                WardNumber = f.House!.Tole!.Ward!.Number,
                MemberCount = _context.Set<Person>()
                    .Where(p => p.Family!.Id == f.Id)
                    .Count()
            })
            .ToListAsync();

        var averageBySizeRange = familiesWithMemberCount
            .GroupBy(f => f.MemberCount > 5 ? "6+ Members" : (f.MemberCount > 3 ? "4-5 Members" : "1-3 Members"))
            .Select(g => new
            {
                SizeRange = g.Key,
                Count = g.Count(),
                Average = g.Average(x => x.MemberCount)
            })
            .ToList();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Average Family Size - {municipality.NameEn}",
            Labels = averageBySizeRange.Select(x => x.SizeRange).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Number of Families",
                    Data = averageBySizeRange.Select(x => x.Count).ToList(),
                    BackgroundColor = "#9b59b6"
                }
            },
            TotalRecords = familiesWithMemberCount.Count
        };
    }

    public async Task<ChartDataResponse> GetHouseholdTrendAsync(Guid municipalityId)
    {
        var municipality = await _context.Set<Municipality>()
            .FirstOrDefaultAsync(m => m.Id == municipalityId);

        if (municipality == null)
            throw new NotFoundException("Municipality", "Id", municipalityId);

        // Placeholder for trend data - would need CreatedAt field on Household
        var trend = new List<(string, int)>
        {
            ("January", 100),
            ("February", 120),
            ("March", 150),
            ("April", 180),
            ("May", 220)
        };

        return new ChartDataResponse
        {
            ChartType = "line",
            Title = $"Household Creation Trend - {municipality.NameEn}",
            Labels = trend.Select(x => x.Item1).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Households Created",
                    Data = trend.Select(x => x.Item2).ToList(),
                    BackgroundColor = "#1abc9c",
                    BorderColor = "#16a085"
                }
            },
            TotalRecords = trend.Sum(x => x.Item2)
        };
    }

    public async Task<ChartDataResponse> GetHouseTypeDistributionAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        // Count houses by type
        var houseTypeDistribution = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null && h.Tole.WardId == wardId)
            .GroupBy(h => h.HouseType != null ? h.HouseType.LabelEn : "Unknown")
            .Select(g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        var totalHouses = houseTypeDistribution.Sum(x => x.Count);

        return new ChartDataResponse
        {
            ChartType = "doughnut",
            Title = $"House Types Distribution - Ward {ward.Number}",
            Description = $"Total Houses: {totalHouses}",
            Labels = houseTypeDistribution.Select(x => x.Type).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = houseTypeDistribution.Select(x => x.Count).ToList(),
                    BackgroundColor = "#3498db,#e74c3c,#2ecc71,#f39c12,#9b59b6"
                }
            },
            TotalRecords = totalHouses
        };
    }

    public async Task<ChartDataResponse> GetRoofTypeDistributionAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        var roofTypeDistribution = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null && h.Tole.WardId == wardId)
            .GroupBy(h => h.RoofType != null ? h.RoofType.LabelEn : "Unknown")
            .Select(g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"Roof Types Distribution - Ward {ward.Number}",
            Labels = roofTypeDistribution.Select(x => x.Type).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = roofTypeDistribution.Select(x => x.Count).ToList()
                }
            },
            TotalRecords = roofTypeDistribution.Sum(x => x.Count)
        };
    }

    public async Task<ChartDataResponse> GetWallMaterialDistributionAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        var wallMaterialDistribution = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null && h.Tole.WardId == wardId)
            .GroupBy(h => h.WallType != null ? h.WallType.LabelEn : "Unknown")
            .Select(g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "bar",
            Title = $"Wall Materials Distribution - Ward {ward.Number}",
            Labels = wallMaterialDistribution.Select(x => x.Type).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = wallMaterialDistribution.Select(x => x.Count).ToList(),
                    BackgroundColor = "#34495e"
                }
            },
            TotalRecords = wallMaterialDistribution.Sum(x => x.Count)
        };
    }

    public async Task<ChartDataResponse> GetLandTypeDistributionAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        var landTypeDistribution = await _context.Set<House>()
            .Where(h => h.Tole != null && h.Tole.Ward != null && h.Tole.WardId == wardId)
            .GroupBy(h => h.LandType != null ? h.LandType.LabelEn : "Unknown")
            .Select(g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return new ChartDataResponse
        {
            ChartType = "doughnut",
            Title = $"Land Types Distribution - Ward {ward.Number}",
            Labels = landTypeDistribution.Select(x => x.Type).ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = landTypeDistribution.Select(x => x.Count).ToList(),
                    BackgroundColor = "#16a085,#d35400,#c0392b"
                }
            },
            TotalRecords = landTypeDistribution.Sum(x => x.Count)
        };
    }

    public async Task<ChartDataResponse> GetHouseQualityAsync(Guid wardId)
    {
        var ward = await _context.Set<Ward>()
            .FirstOrDefaultAsync(w => w.Id == wardId);

        if (ward == null)
            throw new NotFoundException("Ward", "Id", wardId);

        // Placeholder for house quality - would need specific quality indicators
        var qualityData = new Dictionary<string, int>
        {
            { "Good", 45 },
            { "Fair", 35 },
            { "Poor", 20 }
        };

        return new ChartDataResponse
        {
            ChartType = "pie",
            Title = $"House Quality Indicators - Ward {ward.Number}",
            Labels = qualityData.Keys.ToList(),
            Datasets = new List<Dataset>
            {
                new Dataset
                {
                    Label = "Count",
                    Data = qualityData.Values.ToList(),
                    BackgroundColor = "#2ecc71,#f39c12,#e74c3c"
                }
            },
            TotalRecords = qualityData.Values.Sum()
        };
    }
}
