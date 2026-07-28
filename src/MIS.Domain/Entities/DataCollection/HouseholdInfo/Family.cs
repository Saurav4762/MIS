using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.DataCollection.HouseInfo;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Family : BaseEntity
{


    public Guid ResidenceHouseId { get; set; }
    public Guid? ResidentTypeId { get; set; }
    public Guid SubmissionId { get; set; }

    // 1-to-1 Sub-Module Forms
    public House ResidenceHouse { get; set; } = null!;
    public OptionItem? ResidentType { get; set; }
    public Agriculture? Agriculture { get; set; }
    public Decision? Decision { get; set; }
    public Disaster? Disaster { get; set; }
    public Economy? Economic { get; set; }
    public Facility? Facilities { get; set; }
    public Health? Health { get; set; }
    public Livestock? Livestock { get; set; }
    public Social? Social { get; set; }

    // 1-to-Many Relationship: One family has multiple members
    public ICollection<Member> Members { get; set; } = new List<Member>();
    public ICollection<Migration> Migrations { get; set; } = [];

}