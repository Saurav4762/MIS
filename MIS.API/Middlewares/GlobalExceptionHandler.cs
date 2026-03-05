using System.Net;
using System.Text.Json;
using MIS.API.Responses;



namespace MIS.API.Exceptions;

public class GlobalExceptionHandler : IMiddleware
{
  private readonly ILogger<GlobalExceptionHandler> _logger;

  public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
  {
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      await HandleException(context, ex);
    }
  }

  private async Task HandleException(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";

    var response = exception switch
    {
      ValidationException validationEx => HandleBaseException(context, validationEx),
      BadRequestException badRequestEx => HandleBaseException(context, badRequestEx),
      NotFoundException notFoundEx => HandleBaseException(context, notFoundEx),
      ConflictException conflictEx => HandleBaseException(context, conflictEx),
      UnauthorizedException unauthorizedEx => HandleBaseException(context, unauthorizedEx),
      ForbiddenException forbiddenEx => HandleBaseException(context, forbiddenEx),
      InternalServerException internalServerEx => HandleBaseException(context, internalServerEx),
      BaseException baseEx => HandleBaseException(context, baseEx),
      _ => HandleUnknownException(context, exception)
    };

    _logger.LogError(exception, "Exception occured {Method} {Path}: {Message}", context.Request.Method, context.Request.Path, exception.Message);
    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
  }





  private ApiResponse<object> HandleBaseException(HttpContext context, BaseException exception)
  {
    context.Response.StatusCode = (int)exception.StatusCode;

    var response = ApiResponse<object>.FailResponse(exception.ErrorCode, exception.Message, exception.Details, exception.StatusCode);

    return response;
  }

  private ApiResponse<object> HandleUnknownException(HttpContext context, Exception exception)
  {
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var response = ApiResponse<object>.FailResponse("INTERNAL_ERROR", "Something went wrong");
    return response;
  }
}