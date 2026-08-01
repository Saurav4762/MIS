using System.Linq;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Famiiles;

public static class ToFamilyDto
{
    public static FamilyDTO ToDto(Family f)
    {
        return new FamilyDTO
        {
            Id = f.Id,
            SubmissionId = f.SubmissionId,
            ResidenceHouseId = f.ResidenceHouseId,
            ResidentTypeId = f.ResidentTypeId,
            Members = f.Members?.Select(ToMemberDto.ToDto).ToList() ?? new System.Collections.Generic.List<MemberDto>()
        };
    }
}