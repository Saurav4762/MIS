using MIS.Application.Features.DataCollection.HouseholdInfo.Socials;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;  // plural, matches Families/Members pattern

public class SocialService : ISocialService
{
    private readonly ISocialRepo _repo;

    public SocialService(ISocialRepo repo)
    {
        _repo = repo;
    }

    public async Task<Guid> CreateSocialAsync(CreateSocialDto dto)
    {
        var social = new Social  // bare, no collision now
        {
            Id = Guid.NewGuid(),
            FamilyId = dto.FamilyId,
            EthnicityId = dto.EthnicityId,
            ReligionId = dto.ReligionId,
            MotherTongueId = dto.MotherTongueId,
            CommonLanguageId = dto.CommonLanguageId
        };

        await _repo.CreateSocialAsync(social);
        return social.Id;
    }

    public async Task<SocialDto?> GetSocialByIdAsync(Guid id)
    {
        var social = await _repo.GetSocialByIdAsync(id);
        return social == null ? null : ToSocialDto.ToDto(social);
    }
}