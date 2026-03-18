using System.Net;
using System.Text.Json;
using MIS.API.Common.Responses;
using MIS.Domain.Exceptions;

namespace MIS.API.Common.Middlewares;

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
      DataValidationException validationEx => HandleBaseException(context, validationEx, HttpStatusCode.BadRequest),
      BadRequestException badRequestEx => HandleBaseException(context, badRequestEx, HttpStatusCode.BadRequest),
      NotFoundException notFoundEx => HandleBaseException(context, notFoundEx, HttpStatusCode.NotFound),
      ConflictException conflictEx => HandleBaseException(context, conflictEx, HttpStatusCode.Conflict),
      UnauthorizedException unauthorizedEx => HandleBaseException(context, unauthorizedEx, HttpStatusCode.Unauthorized),
      ForbiddenException forbiddenEx => HandleBaseException(context, forbiddenEx, HttpStatusCode.Forbidden),
      InternalServerException internalServerEx => HandleBaseException(context, internalServerEx, HttpStatusCode.InternalServerError),
      BaseException baseEx => HandleBaseException(context, baseEx, HttpStatusCode.BadRequest),
      _ => HandleUnknownException(context, exception)
    };

    _logger.LogError(exception, "Exception occured {Method} {Path}: {Message}", context.Request.Method, context.Request.Path, exception.Message);
    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
  }





  private ApiResponse<object> HandleBaseException(HttpContext context, BaseException exception, HttpStatusCode httpStatusCode)
  {
    context.Response.StatusCode = (int)httpStatusCode;

    var response = ApiResponse<object>.FailResponse(exception.ErrorCode, exception.Message, exception.Details, httpStatusCode);

    return response;
  }

  private ApiResponse<object> HandleUnknownException(HttpContext context, Exception exception)
  {
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var response = ApiResponse<object>.FailResponse("INTERNAL_ERROR", "Something went wrong");
    return response;
  }
}