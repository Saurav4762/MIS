namespace MIS.API.Common.Responses;

public class ApiError
{
  public string Code { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public Dictionary<string, string[]>? Details { get; set; }
  public ApiError() { }
  public ApiError(string code, string message, Dictionary<string, string[]>? details = null)
  {
    Code = code;
    Message = message;
    Details = details;
  }
}