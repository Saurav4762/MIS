using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public static class ToSocialDto
{
    public static SocialDto ToDto(Social social)
    {
        return new SocialDto()
        {
            Id = social.Id,
            FamilyId = social.FamilyId,
            EthnicityId = social.EthnicityId,
            ReligionId = social.ReligionId,
            MotherTongueId = social.MotherTongueId,
            CommonLanguageId = social.CommonLanguageId
        };
    }
}