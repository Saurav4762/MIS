using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Ward
{
  [Key]
  public Guid Id { get; set; }
  public Guid MunicipalityId { get; set; }
  public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;

  public Municipality Municipality { get; set; } = null!;
  public IEnumerable<Tole> Toles { get; set; } = null!;
}