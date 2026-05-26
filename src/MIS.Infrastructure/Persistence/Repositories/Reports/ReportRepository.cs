using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Reports;
using MIS.Domain.Entities.HouseHold;
using MIS.Domain.Entities.Identity;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Reports;

public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalPersonsAsync(Guid municipalityId) =>
        await _context.Set<Person>().CountAsync();

    public async Task<int> GetMaleCountAsync(Guid municipalityId) =>
        await _context.Set<Person>()
            .CountAsync(p => p.Gender != null && p.Gender.ToLower() == "male");

    public async Task<int> GetFemaleCountAsync(Guid municipalityId) =>
        await _context.Set<Person>()
            .CountAsync(p => p.Gender != null && p.Gender.ToLower() == "female");

    public async Task<int> GetOthersCountAsync(Guid municipalityId) =>
        await _context.Set<Person>()
            .CountAsync(p => p.Gender == null ||
                             (p.Gender.ToLower() != "male" &&
                              p.Gender.ToLower() != "female"));

    public async Task<int> GetTotalHouseholdsAsync(Guid municipalityId) =>
        await _context.Set<Family>().CountAsync();

    public async Task<int> GetAge16PlusCountAsync(Guid municipalityId)
    {
        var cutoff = DateTime.UtcNow.AddYears(-16);
        return await _context.Set<Person>()
            .CountAsync(p => p.DateOfBirth <= cutoff);
    }

    public async Task<int> GetLiterateCountAsync(Guid municipalityId) =>
        await _context.Set<Person>()
            .CountAsync(p => p.Educations != null && p.Educations.Any());
}