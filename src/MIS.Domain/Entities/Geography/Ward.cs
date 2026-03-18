using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Ward : BaseEntity
{
  public Guid MunicipalityId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;

  // Navigation Property
  public Municipality Municipality { get; set; } = null!;
  public IEnumerable<Tole> Toles { get; set; } = null!;

}