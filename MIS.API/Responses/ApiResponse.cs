using System.Diagnostics;
using System.Security.Permissions;

namespace MIS.API.Responses;


public class ApiResponse<T>
{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public T? Data { get; set; }
  public ApiError? Error { get; set; }
  public int StatusCode { get; set; }
  public DateTime Timestamp { get; set; } = DateTime.UtcNow;

  public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful", int statusCode = 200)
  {
    return new ApiResponse<T>
    {
      Success = true,
      Message = message,
      Data = data,
      StatusCode = statusCode
    };
  }

  public static ApiResponse<T> FailResponse(string message, ApiError? error = null, int statusCode = 400)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      StatusCode = statusCode,
      Error = error
    };
  }

  public static ApiResponse<T> FailResponse(string errorCode, string message, Dictionary<string, string[]>? details = null, int statusCode = 400)
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