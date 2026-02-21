namespace MIS.API.Models;

public class Municipality
{
  public Guid Id { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
  public IEnumerable<Ward> Wards { get; set; } = null!; 
}