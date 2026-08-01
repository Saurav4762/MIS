using MIS.Application.Features.DataCollection.HouseholdInfo.Members;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public static class ToMemberDto
{
    public static MemberDto ToDto(Member member)
    {
        return new MemberDto
        {
            Id = member.Id,
            FamilyId = member.FamilyId,
            FullNameEn = member.FullNameEn,
            FullNameNe = member.FullNameNe,
            DateOfBirth = member.DateOfBirth,
            MobileNumber = member.MobileNumber,
            Email = member.Email,
            GenderId = member.GenderId,
            MartialStatusId = member.MaritalStatusId,
            RelationshipToHeadId = member.RelationshipToHeadId,
            IdTypeId = member.IdTypeId,
            EducationLevelId = member.EducationLevelId,
            OccupationId = member.OccupationId
        };
    }

    public static Member ToEntity(CreateMemberDto dto, Guid familyId)
    {
        return new Member
        {
            Id = Guid.NewGuid(),
            FamilyId = familyId,
            FullNameEn = dto.FullNameEn,
            FullNameNe = dto.FullNameNe,
            DateOfBirth = dto.DateOfBirth,
            MobileNumber = dto.MobileNumber,
            Email = dto.Email ?? string.Empty,
            GenderId = dto.GenderId,
            MaritalStatusId = dto.MartialStatusId,
            RelationshipToHeadId = dto.RelationshipToHeadId,
            IdTypeId = dto.IdTypeId,
            EducationLevelId = dto.EducationLevelId,
            OccupationId = dto.OccupationId
        };
    }
}
