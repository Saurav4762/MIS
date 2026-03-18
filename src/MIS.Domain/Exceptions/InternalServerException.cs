namespace MIS.Domain.Exceptions;

public class InternalServerException : BaseException
{
  public InternalServerException() : base(
      message: "An unexpected error occurred. Please try again later.",
      errorCode: "INTERNAL_ERROR"
    )
  {
  }
  public InternalServerException(string message = "An unexpected error occurred. Please try again later.") : base(message, errorCode: "INTERNAL_ERROR")
  {

  }
}