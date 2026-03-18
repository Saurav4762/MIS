using MIS.Domain.Common.Premitives;

public class OptionItem : BaseEntity
{
  public Guid OptionListId { get; set; }

  public string LabelEn { get; set; } = null!;

  public string LabelNe { get; set; } = null!;

  public Dictionary<string, object>? Extra { get; set; }

  //Navigation property
  public OptionList OptionList { get; set; } = null!;

}