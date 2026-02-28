namespace MIS.API.DTOs;


public class OptionListRequest
{
  public string Code { get; set; } = null!;
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
}

public class OptionListResponse
{
  public string Code { get; set; } = null!;
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
}