namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public interface IFacilityService
{
    Task<FacilityDto> GetFacilityByFamilyIdAsync(Guid familyId);
    Task<FacilityDto> CreateFacilityAsync(CreateFacilityDto createFacilityDto);
    Task<FacilityDto> UpdateFacilityAsync(Guid familyId, UpdateFacilityDto facilityDto);
}
