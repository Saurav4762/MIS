namespace MIS.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
  public UnauthorizedException() : base(
  message: "You are not authenticated. Please log in",
  errorCode: "UNAUTHORIZED"
  )
  { }

  public UnauthorizedException(string message)
  : base(message: message,  errorCode: "UNAUTHORIZED")
  {

  }
}