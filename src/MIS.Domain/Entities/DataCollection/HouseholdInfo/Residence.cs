using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Residence: BaseEntity
{
    //Foreign
    public Guid FamilyId { get; set; }
     public Guid? ResidentTypeId { get; set; }
     public Guid? LandOwnershipId { get; set; }
    // Navigation 
    public Family Family { get; set; } = null!;
    public OptionItem? ResidentType { get; set; }
    public OptionItem? LandOwnership { get; set; }
}