using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Area : BaseEntity
{
  public Guid DistrictId { get; set; }
  public string Code { get; set; } = null!;
  public string NameEn { get; set; } = null!;
  public string NameNe { get; set; } = null!;
  // Navigation property
  public District District { get; set; } = null!;
  public IEnumerable<Municipality> Municipalities { get; set; } = null!;
}