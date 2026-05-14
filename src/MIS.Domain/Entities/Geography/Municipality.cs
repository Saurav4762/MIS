using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Municipality : BaseEntity
{
  public Guid AreaId { get; set; }
  public string Code { get; set; } = null!;
  public string NameEn { get; set; } = null!;
  public string NameNe { get; set; } = null!;
  public string HeadExecutiveNameEn { get; set; } = null!;
  public string HeadExecutiveNameNe { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PhoneNo { get; set; } = null!;
  public string Website { get; set; } = null!;

  // Navigation property
  public Area Area { get; set; } = null!;
  public IEnumerable<Ward> Wards { get; set; } = null!;
}