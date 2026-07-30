using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class FacilityRepo(ApplicationDbContext context) : IFacilityRepo
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Facility?> GetFacilityByFamilyIdAsync(Guid familyId)
    {
        return await _context.Facilities.FirstOrDefaultAsync(f => f.FamilyId == familyId);
    }

    public async Task<Facility> CreateFacilityAsync(Facility facility)
    {
        _context.Facilities.Add(facility);
        await _context.SaveChangesAsync();
        return facility;
    }

    public async Task<Facility> UpdateFacilityAsync(Facility facility)
    {
        _context.Facilities.Update(facility);
        await _context.SaveChangesAsync();
        return facility;
    }
}
