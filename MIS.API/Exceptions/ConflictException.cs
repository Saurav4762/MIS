namespace MIS.API.Exceptions;


public class ConflictException : BaseException
{
  public ConflictException(string entity, string reason)
  : base(
    message: "The request could not be completed because it conflicts with the current state of the resource.",
    statusCode: 409,
    errorCode: "CONFLICT",
    new Dictionary<string, string[]>
    {
      { entity, [reason]}
    }
  )
  {
  }

  public ConflictException(string message = "The request could not be completed because it conflicts with the current state of the resource.") : base(message, 409, "CONFLICT") { }

}