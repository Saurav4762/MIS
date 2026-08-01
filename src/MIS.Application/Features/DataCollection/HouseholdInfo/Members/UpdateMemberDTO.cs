namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public class UpdateMemberDTO
{
    public string? FullNameEn { get; set; }
    public string? FullNameNe { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? MobileNumber { get; set; }
    public string? Email { get; set; }
    public Guid? GenderId { get; set; }
    public Guid? MartialStatusId { get; set; }
    public Guid? RelationshipToHeadId { get; set; }
    public Guid? IdTypeId { get; set; }
    public Guid? EducationLevelId { get; set; }
    public Guid? OccupationId { get; set; }
}
