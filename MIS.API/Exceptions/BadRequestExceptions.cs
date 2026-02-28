namespace MIS.API.Exceptions;

public class BadRequestException : BaseException
{
  public BadRequestException(string message)
    : base(message, statusCode: 400, errorCode: "BAD_REQUEST")
  { }

  public BadRequestException(string message, Dictionary<string, string[]> details)
  : base(message, statusCode: 400, errorCode: "BAD_REQUEST", details)
  { }
}