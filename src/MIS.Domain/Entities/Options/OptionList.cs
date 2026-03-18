using MIS.Domain.Common.Premitives;

public class OptionList : BaseEntity
{
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public ICollection<OptionItem> OptionItems { get; set; } = [];

}