namespace MIS.Domain.Exceptions;

public class ForbiddenException : BaseException
{
  public ForbiddenException(string message = "You do not have permission to access this resource.")
  : base(
     message: message,
     errorCode: "FORBIDDEN"
    )
  { }

}