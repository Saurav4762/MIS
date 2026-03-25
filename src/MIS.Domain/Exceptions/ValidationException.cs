namespace MIS.Domain.Exceptions;


public class DataValidationException : BaseException
{
  public Dictionary<int, Dictionary<string, string[]>>? RowValidationErrors { get; set; }

  public DataValidationException(Dictionary<int, Dictionary<string, string[]>> rowValidationErrors) : base("One or more validation error occured", "VALIDATION_ERROR")
  {
    RowValidationErrors = rowValidationErrors;
  }

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