using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public interface ISocialRepo
{
    Task CreateSocialAsync(Domain.Entities.DataCollection.HouseholdInfo.Social social);
    Task<Social?> GetSocialByIdAsync(Guid id);
}