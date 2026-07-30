using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public interface IFacilityRepo
{
    Task<Facility?> GetFacilityByFamilyIdAsync(Guid familyId);
    Task<Facility> CreateFacilityAsync(Facility facility);
    Task<Facility> UpdateFacilityAsync(Facility facility);
}
