using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Tole
{
  [Key]
  public Guid Id { get; set; }
  public Guid WardId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
  
  //  Navigation properties
  public Ward Ward { get; set; } = null!;
  public IEnumerable<House> Houses { get; set; } = null!;
}
