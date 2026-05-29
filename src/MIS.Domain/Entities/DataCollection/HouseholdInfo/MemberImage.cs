using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class MemberImage : BaseEntity
{
    public Guid MemberId { get; set; }
    public string ImageUrl { get; set; } = null!;
}