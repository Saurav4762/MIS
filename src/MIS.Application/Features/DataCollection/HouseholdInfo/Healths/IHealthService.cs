namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public interface IHealthService
{
    Task<HealthDto> GetHealthByFamilyIdAsync(Guid familyId);
    Task<HealthDto> CreateHealthAsync(CreateHealthDto createHealthDto);
    Task<HealthDto> UpdateHealthAsync(Guid familyId, UpdateHealthDto updateHealthDto);
}
