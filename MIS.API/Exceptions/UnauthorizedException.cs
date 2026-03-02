using System.Net;

namespace MIS.API.Exceptions;

public class UnauthorizedException : BaseException
{
  public UnauthorizedException() : base(
  message: "You are not authenticated. Please log in",
  statusCode: HttpStatusCode.Unauthorized,
  errorCode: "UNAUTHORIZED"
  )
  { }

  public UnauthorizedException(string message)
  : base(message: message, statusCode: HttpStatusCode.Unauthorized, errorCode: "UNAUTHORIZED")
  {

  }
}