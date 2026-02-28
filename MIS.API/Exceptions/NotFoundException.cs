using System.Diagnostics;

namespace MIS.API.Exceptions;



public class NotFoundException : BaseException
{
  public NotFoundException(string entity, object key)
  : base(
      message: $"{entity} with identifier '{key}' was not found",
      statusCode: 404,
      errorCode: "NOT_FOUND"
  ) { }
}