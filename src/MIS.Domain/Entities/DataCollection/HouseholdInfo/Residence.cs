using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Residence: BaseEntity
{
    //Foreign
    public Guid FamilyId { get; set; }
     public Guid? ResidentTypeId { get; set; }
     public Guid? LandOwnershipId { get; set; }
     public Guid? PreviousDistrictId { get; set; }
     public Guid? PreviousMunicipalityId { get; set; }
     public Guid? ReasonOfMigrationId { get; set; }
    // Navigation 
    public Family Family { get; set; } = null!;
    public OptionItem? ResidentType { get; set; }
    public OptionItem? LandOwnership { get; set; }
    public OptionItem? PreviousDistrict { get; set; }
    public OptionItem? PreviousMunicipality { get; set; }
    public OptionItem? ReasonOfMigration { get; set; }
}