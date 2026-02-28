namespace MIS.API.Exceptions;

public class InternalServerException : BaseException
{
  public InternalServerException() : base(
      message: "An unexpected error occurred. Please try again later.",
      statusCode: 500,
      errorCode: "INTERNAL_ERROR"
    )
  {
  }
  public InternalServerException(string message = "An unexpected error occurred. Please try again later.") : base(message, statusCode: 500, errorCode: "INTERNAL_ERROR")
  {

  }
}