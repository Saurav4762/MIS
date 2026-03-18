namespace MIS.Domain.Exceptions;


public class DataValidationException : BaseException
{
  public DataValidationException(
    Dictionary<string, string[]> errors
  ) : base(
      message: "One or more validation error occured.",
      errorCode: "VALIDATION_ERROR",
      details: errors
    )
  { }

  public DataValidationException(string field, string error)
  : base(
    message: "A validation error occured.",
    errorCode: "VALIDATION_ERROR",
    details: new Dictionary<string, string[]>
    {
      {field, [error]}
    }
  )
  { }




}