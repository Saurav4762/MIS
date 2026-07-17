namespace MIS.Application.Features.DataCollection.Members;

public class MembersDTO
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid GenderId { get; set; }
    public Guid MaritalStatusId { get; set; }
    public Guid RelationTypeId { get; set; }
    public Guid IdTypeId { get; set; }
    public Guid EducationLevelId { get; set; }
    public Guid OccupationId { get; set; }

    public string FullNameEn { get; set; } = null!;
    public string FullNameNe { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = string.Empty;

    
    

}