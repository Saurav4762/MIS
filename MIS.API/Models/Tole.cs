namespace MIS.API.Models;

public class Tole
{
  public Guid id { get; set; }
  public Guid AreaId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
  
  //  Navigation properties
}