namespace MIS.Application.Features.Options.OptionItems;


public class OptionItemDTO
{
  public Guid OptionListId { get; set; }
  public Guid Id { get; set; }
  public string LabelEn { get; set; } = null!;

  public string LabelNe { get; set; } = null!;

  public Dictionary<string, object>? Extra { get; set; }

}