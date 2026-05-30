using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Reports;
using MIS.Domain.Entities.Educations;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Entities.HouseHold;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Reports;

public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;
    public ReportRepository(ApplicationDbContext context) => _context = context;

    public async Task<int> GetTotalPersonsAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync();

    public async Task<int> GetMaleCountAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync(p => p.Gender != null && p.Gender.ToLower() == "male");

    public async Task<int> GetFemaleCountAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync(p => p.Gender != null && p.Gender.ToLower() == "female");

    public async Task<int> GetOthersCountAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync(p => p.Gender == null ||
            (p.Gender.ToLower() != "male" && p.Gender.ToLower() != "female"));

    public async Task<int> GetTotalHouseholdsAsync(Guid municipalityId) =>
        await _context.Set<Family>().CountAsync();

    public async Task<int> GetAge16PlusCountAsync(Guid municipalityId)
    {
        var cutoff = DateTime.UtcNow.AddYears(-16);
        return await _context.Set<Person>().CountAsync(p => p.DateOfBirth <= cutoff);
    }

    public async Task<int> GetLiterateCountAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync(p => p.Educations != null && p.Educations.Any());

    public async Task<List<ChartItem>> GetAgeGroupDistributionAsync()
    {
        var now = DateTime.UtcNow;
        var dates = await _context.Set<Person>().Select(p => p.DateOfBirth).ToListAsync();
        return new List<ChartItem>
        {
            new("0–14", dates.Count(d => (now - d).TotalDays / 365.25 < 15)),
            new("15–24", dates.Count(d => { var a = (now - d).TotalDays / 365.25; return a >= 15 && a < 25; })),
            new("25–44", dates.Count(d => { var a = (now - d).TotalDays / 365.25; return a >= 25 && a < 45; })),
            new("45–64", dates.Count(d => { var a = (now - d).TotalDays / 365.25; return a >= 45 && a < 65; })),
            new("65+",   dates.Count(d => (now - d).TotalDays / 365.25 >= 65)),
        };
    }

    public async Task<List<ChartItem>> GetBloodGroupDistributionAsync() =>
        await _context.Set<Person>()
            .Where(p => p.BloodGroup != null && p.BloodGroup != "")
            .GroupBy(p => p.BloodGroup)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetHouseTypeDistributionAsync() =>
        await _context.Set<House>()
            .Include(h => h.HouseType)
            .GroupBy(h => h.HouseType.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetRoofTypeDistributionAsync() =>
        await _context.Set<House>()
            .Include(h => h.RoofType)
            .GroupBy(h => h.RoofType.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetWallTypeDistributionAsync() =>
        await _context.Set<House>()
            .Include(h => h.WallType)
            .GroupBy(h => h.WallType.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetLandTypeDistributionAsync() =>
        await _context.Set<House>()
            .Include(h => h.LandType)
            .GroupBy(h => h.LandType.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetFamilyByEthnicityAsync() =>
        await _context.Set<Family>()
            .Include(f => f.Ethnicity)
            .Where(f => f.Ethnicity != null)
            .GroupBy(f => f.Ethnicity!.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetFamilyByReligionAsync() =>
        await _context.Set<Family>()
            .Include(f => f.Religion)
            .Where(f => f.Religion != null)
            .GroupBy(f => f.Religion!.LabelEn)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetEducationProgramsAsync() =>
        await _context.Set<Education>()
            .Where(e => e.Program != null && e.Program != "")
            .GroupBy(e => e.Program)
            .Select(g => new ChartItem(g.Key, g.Count()))
            .ToListAsync();

    public async Task<List<ChartItem>> GetWardHouseholdCountAsync() =>
        await _context.Set<Ward>()
            .Select(w => new ChartItem($"Ward {w.Number}", w.Toles.Count()))
            .ToListAsync();
}