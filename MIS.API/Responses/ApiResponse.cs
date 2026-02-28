using System.Diagnostics;

namespace MIS.API.Responses;


public class ApiResponse<T>
{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public T? Data { get; set; }
  public List<ApiError>? Errors { get; set; }
  public int StatusCode { get; set; }
  public DateTime Timestamp { get; set; } = DateTime.UtcNow;

  public static ApiResponse<T> SuccessResponse(T data, string message = " Request successful", int statusCode = 200)
  {
    return new ApiResponse<T>
    {
      Success = true,
      Message = message,
      Data = data,
      StatusCode = statusCode
    };
  }
  public static ApiResponse<T> FailResponse(string message, int statusCode = 400, List<ApiError>? errors = null)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      Errors = errors,
      StatusCode = statusCode
    };
  }

  public static ApiResponse<T> FailResponse(string message, string errorCode, string errorDetail, string? field = null, int statusCode = 400)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      StatusCode = statusCode,
      Errors = new List<ApiError> { new ApiError(errorCode, errorDetail, field) }
    };
  }

  public static ApiResponse<T> FailResponse(string message, string errorCode, string errorDetail, int statusCode = 400, string? field = null)
  {
    return new ApiResponse<T>
    {
      Success = false,
      Message = message,
      StatusCode = statusCode,
      Errors = new List<ApiError> { new ApiError(errorCode, errorDetail, field) }
    };
  }

}