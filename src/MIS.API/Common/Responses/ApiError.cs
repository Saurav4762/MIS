namespace MIS.API.Common.Responses;

public class ApiError
{
  public string Code { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public Dictionary<string, string[]>? Details { get; set; }
  public Dictionary<int, Dictionary<string, string[]>>? RowErrors { get; set; }
  public ApiError() { }
  public ApiError(string code, string message, Dictionary<string, string[]>? details = null, Dictionary<int, Dictionary<string, string[]>>? rowErrors = null)
  {
    Code = code;
    Message = message;
    Details = details;
    RowErrors = rowErrors;
  }
}