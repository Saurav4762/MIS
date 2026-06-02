using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Area : BaseEntity
{
  public Guid DistrictId { get; set; }
  public int Number { get; set; } = 0;
  // Navigation property
  public District District { get; set; } = null!;
  public IEnumerable<Municipality> Municipalities { get; set; } = null!;
}