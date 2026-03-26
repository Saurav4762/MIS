namespace MIS.Application.Features.Options.OptionLists;

public class UpdateOptionListDTO
{
  public string? LabelEn { get; set; }
  public string? LabelNe { get; set; }
  public string? Description { get; set; }
  public Dictionary<string, object>? Extra { get; set; }
}