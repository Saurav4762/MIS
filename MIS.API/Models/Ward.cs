namespace MIS.API.Models;

public class Ward
{
  public Guid id { get; set; }
  public Guid MunicipalityId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;

  public Municipality Municipality { get; set; } = null!;
  public IEnumerable<Area> Areas { get; set; } = null!;
}