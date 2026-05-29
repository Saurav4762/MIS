using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Family : BaseEntity
{

    public Guid HouseId { get; set; }
    public House_info.House House { get; set; } = null!;
    
    // 1-to-1 Sub-Module Forms
    public Agriculture? Agriculture { get; set; }
    public Decision? Decision { get; set; }
    public Disaster? Disaster { get; set; }
    public Economic? Economic { get; set; }
    public Facilities? Facilities { get; set; }
    public Health? Health { get; set; }
    public Livestock? Livestock { get; set; }
    public Residence? Residence { get; set; }
    public Social? Social { get; set; }

    // 1-to-Many Relationship: One family has multiple members
    public ICollection<Member> Members { get; set; } = new List<Member>();

}