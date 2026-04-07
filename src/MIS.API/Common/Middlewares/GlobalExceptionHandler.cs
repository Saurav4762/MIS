using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using MIS.API.Common.Responses;
using MIS.Domain.Exceptions;
using Npgsql;

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
      DataValidationException validationEx => HandleDataValidationException(context, validationEx),
      BadRequestException badRequestEx => HandleBaseException(context, badRequestEx, HttpStatusCode.BadRequest),
      NotFoundException notFoundEx => HandleBaseException(context, notFoundEx, HttpStatusCode.NotFound),
      ConflictException conflictEx => HandleBaseException(context, conflictEx, HttpStatusCode.Conflict),
      UnauthorizedException unauthorizedEx => HandleBaseException(context, unauthorizedEx, HttpStatusCode.Unauthorized),
      ForbiddenException forbiddenEx => HandleBaseException(context, forbiddenEx, HttpStatusCode.Forbidden),
      InternalServerException internalServerEx => HandleBaseException(context, internalServerEx, HttpStatusCode.InternalServerError),
      BaseException baseEx => HandleBaseException(context, baseEx, HttpStatusCode.BadRequest),
      DbUpdateException dbUpdateEx => HandleDbUpdateException(context, dbUpdateEx),
      _ => HandleUnknownException(context, exception)
    };

    _logger.LogError(exception, "Exception occured {Method} {Path}: {Message}", context.Request.Method, context.Request.Path, exception.Message);
    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
  }



  private ApiResponse<object> HandleDataValidationException(HttpContext context, DataValidationException exception)
  {
    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    var response = ApiResponse<object>.FailResponse(exception.ErrorCode, exception.Message, exception.Details, exception.RowValidationErrors, HttpStatusCode.BadRequest);

    return response;
  }





  private ApiResponse<object> HandleBaseException(HttpContext context, BaseException exception, HttpStatusCode httpStatusCode)
  {
    context.Response.StatusCode = (int)httpStatusCode;


    var response = ApiResponse<object>.FailResponse(exception.ErrorCode, exception.Message, exception.Details, null, httpStatusCode);

    return response;
  }

  private ApiResponse<object> HandleUnknownException(HttpContext context, Exception exception)
  {
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var response = ApiResponse<object>.FailResponse("INTERNAL_ERROR", "Something went wrong");
    return response;
  }

  private ApiResponse<object> HandleDbUpdateException(HttpContext context, DbUpdateException exception)
  {
    if (exception.InnerException is PostgresException postgresEx &&
        postgresEx.SqlState == PostgresErrorCodes.UniqueViolation)
    {
      context.Response.StatusCode = (int)HttpStatusCode.Conflict;

      var details = BuildUniqueViolationDetails(postgresEx.ConstraintName, postgresEx.Detail);

      return ApiResponse<object>.FailResponse(
        "CONFLICT",
        "A record with the same unique value already exists.",
        details,
        null,
        HttpStatusCode.Conflict
      );
    }

    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return ApiResponse<object>.FailResponse("INTERNAL_ERROR", "A database error occurred while processing the request.");
  }

  private static Dictionary<string, string[]> BuildUniqueViolationDetails(string? constraintName, string? postgresDetail)
  {
    if (TryParseDuplicateDetail(postgresDetail, out var parsedKey, out var parsedValue))
    {
      return new Dictionary<string, string[]>
      {
        { "key", [parsedKey] },
        { "value", [parsedValue] }
      };
    }

    if (string.IsNullOrWhiteSpace(constraintName))
    {
      return new Dictionary<string, string[]>
      {
        { "key", ["unknown"] },
        { "value", ["unknown"] }
      };
    }

    // Expected index pattern: IX_<TableName>_<ColumnName>
    var parts = constraintName.Split('_', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length >= 3)
    {
      var columnName = string.Join('_', parts.Skip(2));
      return new Dictionary<string, string[]>
      {
        { "key", [columnName] },
        { "value", ["unknown"] }
      };
    }

    return new Dictionary<string, string[]>
    {
      { "key", [constraintName] },
      { "value", ["unknown"] }
    };
  }

  private static bool TryParseDuplicateDetail(string? detail, out string key, out string value)
  {
    key = string.Empty;
    value = string.Empty;

    if (string.IsNullOrWhiteSpace(detail))
    {
      return false;
    }

    // Npgsql detail format: Key (NameNe)=(Kathmandu) already exists.
    var match = Regex.Match(detail, @"Key \((?<key>[^)]+)\)=\((?<value>[^)]*)\) already exists\.", RegexOptions.CultureInvariant);
    if (!match.Success)
    {
      return false;
    }

    key = match.Groups["key"].Value;
    value = match.Groups["value"].Value;
    return true;
  }
}