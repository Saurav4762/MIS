using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Agriculture : BaseEntity
{
    public Guid FamilyId { get; set; }
    
    //Navigation 
    public Family Family { get; set; } = null!;
}