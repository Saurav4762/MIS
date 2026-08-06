using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Social : BaseEntity
{
    // Foreign
    public Guid FamilyId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid ReligionId { get; set; }
    public Guid MotherTongueId { get; set; }
    public Guid CommonLanguageId { get; set; }
    
    //Navigation property
    public Family Family { get; set; } = null!;
    public OptionItem? Ethnicity { get; set; }
    public OptionItem? Religion { get; set; } 
    public OptionItem? MotherTongue { get; set; } 
    public OptionItem? CommonLanguage { get; set; } 
}