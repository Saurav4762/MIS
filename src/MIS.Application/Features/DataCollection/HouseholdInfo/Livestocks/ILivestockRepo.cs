using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public interface ILivestockRepo
{
    Task<Livestock?> GetLivestockByFamilyIdAsync(Guid familyId);
    Task<Livestock> CreateLivestockAsync(Livestock livestock);
}
