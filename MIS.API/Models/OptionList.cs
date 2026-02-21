using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;


public class OptionList
{

  [Key]
  public Guid Id { get; set; }
  public string Code { get; set; } = null!;
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public IEnumerable<OptionItem> OptionItems { get; set; } = null!;
}