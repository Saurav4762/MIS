using System.Diagnostics;
using System.Collections;
using System.Net;
using System.Security.Permissions;

namespace MIS.API.Common.Responses;


public class ApiResponse<T>
{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public T? Data { get; set; }
  public ApiError? Error { get; set; }
  public HttpStatusCode StatusCode { get; set; }
  public DateTime Timestamp { get; set; } = DateTime.UtcNow;
  public int? Page { get; set; }
  public int? PageSize { get; set; }
  public int? TotalCount { get; set; }
  public bool? HasNext { get; set; }
  public bool? HasPrevious { get; set; }
  public string? NextCursor { get; set; }
  public string? PreviousCursor { get; set; }


  public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful", HttpStatusCode statusCode = HttpStatusCode.OK)
  {
    return new ApiResponse<T>
    {
      Success = true,
      Message = message,
      Data = data,
      StatusCode = statusCode
    };
  }
  // -------------------------------
  public static ApiResponse<IEnumerable<T>> Paginated(
      IEnumerable<T> data,
      int page,
      int pageSize,
      int? totalCount = null,
      string? nextCursor = null,
      string? previousCursor = null,
      string message = "Success",
      HttpStatusCode statusCode = HttpStatusCode.OK
  )
  {

    var hasNext = totalCount.HasValue
        ? page * pageSize < totalCount.Value
        : data.Count() == pageSize; // fallback: assume next page exists if current page is full

    var hasPrevious = page > 1 || previousCursor != null;

    return new ApiResponse<IEnumerable<T>>
    {
      Success = true,
      Message = message,
      Data = data,
      StatusCode = statusCode,
      Page = page,
      PageSize = pageSize,
      TotalCount = totalCount,
      HasNext = hasNext,
      HasPrevious = hasPrevious,
      NextCursor = nextCursor,
      PreviousCursor = previousCursor
    };
  }




  public static ApiResponse<T> FailResponse(string message, ApiError? error = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      StatusCode = statusCode,
      Error = error
    };
  }

  public static ApiResponse<T> FailResponse(string errorCode, string message, Dictionary<string, string[]>? details = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      StatusCode = statusCode,
      Error = new ApiError(errorCode, message, details)
    };
  }

}