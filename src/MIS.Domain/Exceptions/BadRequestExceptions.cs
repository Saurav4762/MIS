namespace MIS.Domain.Exceptions;

public class BadRequestException : BaseException
{
  public BadRequestException() : base("something went wrong", "BAD_REQUEST")
  {
  }
  public BadRequestException(string message)
    : base(message,  errorCode: "BAD_REQUEST")
  { }

  public BadRequestException(string message, Dictionary<string, string[]> details)
  : base(message, errorCode: "BAD_REQUEST", details)
  { }
}