using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
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

    public async Task<Facility> UpdateFacilityAsync(Guid familyId, Facility facility)
    {
        var existingFacility = await _context.Facilities.FirstOrDefaultAsync(f => f.FamilyId == familyId);

        if (existingFacility is null)
        {
            throw new InvalidOperationException($"Facility for family '{familyId}' was not found.");
        }

        existingFacility.DrinkingWaterId = facility.DrinkingWaterId;
        existingFacility.ToiletTypeId = facility.ToiletTypeId;
        existingFacility.ElectricityId = facility.ElectricityId;
        existingFacility.AltLightId = facility.AltLightId;
        existingFacility.CookingFuelId = facility.CookingFuelId;
        existingFacility.StoveTypeId = facility.StoveTypeId;
        existingFacility.MobilePhone = facility.MobilePhone;
        existingFacility.Radio = facility.Radio;
        existingFacility.Television = facility.Television;
        existingFacility.Computer = facility.Computer;
        existingFacility.Internet = facility.Internet;
        existingFacility.Refrigerator = facility.Refrigerator;
        existingFacility.WashingMachine = facility.WashingMachine;

        await _context.SaveChangesAsync();
        return existingFacility;
    }
}
