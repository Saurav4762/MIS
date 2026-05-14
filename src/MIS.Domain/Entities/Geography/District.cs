using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class District : BaseEntity
{
  public Guid ProvinceId { get; set; }
  public string Code { get; set; } = null!;
  public string NameEn { get; set; } = null!;
  public string NameNe { get; set; } = null!;

  // Navigation properties
  public Province Province { get; set; } = null!;
  public IEnumerable<Area> Areas { get; set; } = null!;
}
