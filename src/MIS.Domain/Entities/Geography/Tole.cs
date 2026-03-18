using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Geography;

public class Tole : BaseEntity
{
  public Guid WardId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;

  // Navitation property
  public Ward Ward { get; set; } = null!;

}
