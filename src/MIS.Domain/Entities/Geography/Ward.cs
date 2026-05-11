using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Ward : BaseEntity
{
  public Guid MunicipalityId { get; set; }
  public int Number { get; set; }
  public int Code { get; set; }
  public string RepresentativeNameEn { get; set; } = string.Empty;
  public string RepresentativeNameNe { get; set; } = string.Empty;
  public string PhoneNo { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;

  // Navigation Property
  public Municipality Municipality { get; set; } = null!;
  public IEnumerable<Tole> Toles { get; set; } = null!;

}