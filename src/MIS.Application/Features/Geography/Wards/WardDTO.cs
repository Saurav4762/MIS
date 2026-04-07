

namespace MIS.Application.Features.Geography.Wards;

public class WardDTO
{
  public Guid Id { get; set; }
  public Guid MunicipalityId { get; set; }
  public int Number { get; set; }
  public string PhoneNo { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string RepresentativeNameEn { get; set; } = string.Empty;
  public string RepresentativeNameNe { get; set; } = string.Empty;
}