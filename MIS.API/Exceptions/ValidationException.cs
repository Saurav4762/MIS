namespace MIS.API.Exceptions;


public class ValidationException : BaseException
{
  public ValidationException(
    Dictionary<string, string[]> errors
  ) : base(
      message: "One or more validation error occured.",
      statusCode: 422,
      errorCode: "VALIDATION_ERROR",
      details: errors
    )
  { }

  public ValidationException(string field, string error)
  : base(
    message: "A validation error occured.",
    statusCode: 422,
    errorCode: "VALIDATION_ERROR",
    details: new Dictionary<string, string[]>
    {
      {field, [error]}
    }
  )
  { }




}