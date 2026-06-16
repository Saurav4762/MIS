using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Migration : BaseEntity
{
    //Foreign
    public Guid FamilyId { get; set; }
    public Guid? PreviousMunicipalityId { get; set; }
    public Guid? ReasonOfMigrationId { get; set; }

    // Navigation 
    public Family Family { get; set; } = null!;
    public OptionItem? PreviousMunicipality { get; set; }
    public OptionItem? ReasonOfMigration { get; set; }
}