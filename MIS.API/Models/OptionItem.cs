using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIS.API.Models;

public class OptionItem
{
  [Key]
  public Guid Id { get; set; }
  public Guid OptionListId { get; set; }

  public string Code { get; set; } = null!;

  public string LabelEn { get; set; } = null!;

  public string LabelNe { get; set; } = null!;

  public Dictionary<string, object>? Extra { get; set; }

  public int? SortOrder { get; set; }

  public bool IsActive = false;

  //Navigation property
  public OptionList OptionList { get; set; } = null!;

}