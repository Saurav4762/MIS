using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public interface IHealthRepo
{
    Task<Health?> GetHealthByFamilyIdAsync(Guid familyId);
    Task<Health> CreateHealthAsync(Health health);
}
