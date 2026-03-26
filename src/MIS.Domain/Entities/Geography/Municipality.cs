using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Municipality : BaseEntity
{
  public string Code { get; set; } = null!;
  public string NameEn { get; set; } = null!;
  public string NameNe { get; set; } = null!;

  // Navigation property
  public IEnumerable<Ward> Wards { get; set; } = null!;
}