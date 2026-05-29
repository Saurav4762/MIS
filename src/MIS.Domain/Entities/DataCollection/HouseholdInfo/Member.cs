using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Member : BaseEntity
{
    //Foreign
    public Guid FamilyId { get; set; }
    public Guid GenderId { get; set; }
    public Guid MaritalStatusId { get; set; }
    public Guid RelationshipToHeadId { get; set; }
    public Guid IdTypeId { get; set; }
    public Guid EducationLevelId { get; set; }
    public Guid OccupationId { get; set; }
    public Guid? ImageId { get; set; }
    
    //Personal info
    public string FullNameEn { get; set; } = null!;
    public string FullNameNe { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    
    //Contact
    public string MobileNumber { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    
    //Navigation
    public Family? Family { get; set; } = null!;
    public OptionItem? Gender { get; set; } = null;
    public OptionItem? MaritalStatus { get; set; } = null;
    public OptionItem? RelationshipToHead { get; set; } = null;
    public OptionItem? IdType { get; set; } = null;
    public OptionItem? EducationLevel { get; set; } = null;
    public OptionItem? Occupation { get; set; } = null;
    public MemberImage? Image { get; set; }
}