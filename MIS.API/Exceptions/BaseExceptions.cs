namespace MIS.API.Exceptions;


public abstract class BaseException : Exception
{
  public int StatusCode { get; }
  public string ErrorCode { get; }
  public Dictionary<string, string[]>? Details { get; }

  protected BaseException(string message, int statusCode, string errorCode, Dictionary<string, string[]>? details = null) : base(message)
  {
    StatusCode = statusCode;
    ErrorCode = errorCode;
    Details = details;
  }
}