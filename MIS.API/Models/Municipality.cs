using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class Municipality
{
  [Key]
  public Guid Id { get; set; }
  public string Code { get; set; } = null!;
  public string NameEn { get; set; } = null!;
  public string NameNe { get; set; } = null!;
  public IEnumerable<Ward> Wards { get; set; } = null!; 
}