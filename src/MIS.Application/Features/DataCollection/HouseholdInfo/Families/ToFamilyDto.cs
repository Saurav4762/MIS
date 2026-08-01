using System.Linq;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public static class ToFamilyDto
{
    public static FamilyDto ToDto(Family f)
    {
        return new FamilyDto
        {
            Id = f.Id,
            SubmissionId = f.SubmissionId,
            ResidenceHouseId = f.ResidenceHouseId,
            ResidentTypeId = f.ResidentTypeId,
            Members = f.Members?.Select(m => new FamilyMemberDto
            {
                Id = m.Id,
                FamilyId = m.FamilyId,
                FullNameEn = m.FullNameEn,
                FullNameNe = m.FullNameNe,
                DateOfBirth = m.DateOfBirth,
                MobileNumber = m.MobileNumber,
                Email = m.Email,
                GenderId = m.GenderId,
                MartialStatusId = m.MaritalStatusId,
                RelationshipToHeadId = m.RelationshipToHeadId,
                IdTypeId = m.IdTypeId,
                EducationLevelId = m.EducationLevelId,
                OccupationId = m.OccupationId
            }).ToList() ?? new System.Collections.Generic.List<FamilyMemberDto>()
        };
    }
}