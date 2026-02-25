namespace MIS.API.Models;

public class Tole
{
  public Guid Id { get; set; }
  public Guid WardId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
  
  //  Navigation properties
}