using System.Diagnostics;
using System.Net;

namespace MIS.API.Exceptions;



public class NotFoundException : BaseException
{
  public NotFoundException(string entity, object key, object value)
  : base(
      message: $"{entity} not found",
      statusCode: HttpStatusCode.NotFound,
      errorCode: "NOT_FOUND",
      new Dictionary<string, string[]>
      {
        {entity, [$"{key} with '${value}' was not found."]}
      }
  )
  {}
}