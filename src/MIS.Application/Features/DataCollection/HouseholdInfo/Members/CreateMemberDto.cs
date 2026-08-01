using System;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public class CreateMemberDto
{
    public Guid FamilyId { get; set; }
    public string FullNameEn { get; set; } = null!;
    public string FullNameNe { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string MobileNumber { get; set; } = null!;
    public string? Email { get; set; }
    
    //option reference
    public Guid? GenderId { get; set; }
    public Guid? MartialStatusId { get; set; }
    public Guid? RelationshipToHeadId { get; set; }
    public Guid? IdTypeId { get; set; }
    public Guid? EducationLevelId { get; set; }
    public Guid? OccupationId { get; set; }
}