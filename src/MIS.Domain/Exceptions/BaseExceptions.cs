namespace MIS.Domain.Exceptions;


public abstract class BaseException : Exception
{
  public string ErrorCode { get; }
  public Dictionary<string, string[]>? Details { get; }

  protected BaseException(string message, string errorCode, Dictionary<string, string[]>? details = null) : base(message)
  {
    ErrorCode = errorCode;
    Details = details;
  }
}