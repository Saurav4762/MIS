using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Health: BaseEntity
{
    public Guid FamilyId { get; set; }
     
    // Navigation 
    public Family Family { get; set; } = null!;
}