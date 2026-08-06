namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public class FamilyMemberDto
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public string FullNameEn { get; set; } = null!;
    public string FullNameNe { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string MobileNumber { get; set; } = null!;
    public string? Email { get; set; }

    public Guid? GenderId { get; set; }
    public Guid? MartialStatusId { get; set; }
    public Guid? RelationshipToHeadId { get; set; }
    public Guid? IdTypeId { get; set; }
    public Guid? EducationLevelId { get; set; }
    public Guid? OccupationId { get; set; }
}


