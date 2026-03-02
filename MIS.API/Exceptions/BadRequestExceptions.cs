using System.Net;

namespace MIS.API.Exceptions;

public class BadRequestException : BaseException
{
  public BadRequestException(string message)
    : base(message, statusCode: HttpStatusCode.BadRequest, errorCode: "BAD_REQUEST")
  { }

  public BadRequestException(string message, Dictionary<string, string[]> details)
  : base(message, statusCode: HttpStatusCode.BadRequest, errorCode: "BAD_REQUEST", details)
  { }
}