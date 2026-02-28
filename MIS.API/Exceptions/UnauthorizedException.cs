namespace MIS.API.Exceptions;

public class UnauthorizedException : BaseException
{
  public UnauthorizedException() : base(
  message: "You are not authenticated. Please log in",
  statusCode: 401,
  errorCode: "UNAUTHORIZED"
  )
  { }

  public UnauthorizedException(string message)
  : base(message: message, statusCode: 401, errorCode: "UNAUTHORIZED")
  {

  }
}