namespace MIS.Domain.Exceptions;

public class NotFoundException : BaseException
{
  public NotFoundException(string entity, object key, object value)
  : base(
      message: $"{entity} not found",
      errorCode: "NOT_FOUND",
      new Dictionary<string, string[]>
      {
        {entity, [$"{key} with '${value}' was not found."]}
      }
  )
  {}
}