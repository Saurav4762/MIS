using MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public interface ISocialService
{
    Task<Guid> CreateSocialAsync(CreateSocialDto dto);
    Task<SocialDto?> GetSocialByIdAsync(Guid id);
}