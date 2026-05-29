using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Social : BaseEntity
{
    // Foreign
    public Guid FamilyId { get; set; }
    public Guid MemberId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid ReligionId { get; set; }
    public Guid MotherTongueId { get; set; }
    public Guid CommonLanguageId { get; set; }
    
    //Navigation property
    public Family Family { get; set; } = null!;
    public OptionItem? Member { get; set; } = null!;
    public OptionItem? Ethnicity { get; set; }
    public OptionItem? Religion { get; set; } = null!;
    public OptionItem? MotherTongue { get; set; } = null!;
    public OptionItem? CommonLanguage { get; set; } = null!;
}