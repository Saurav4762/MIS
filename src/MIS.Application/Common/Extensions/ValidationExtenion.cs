using FluentValidation;
using FluentValidation.Results;
using MIS.Domain.Exceptions;

namespace MIS.Application.Common.Extensions;

public static class ValidationExtensions
{
  public static async Task EnsureValidOrThrowAsync<T>(
    this IValidator<T> validator,
    T model,
    CancellationToken cancellationToken = default)
  {
    if(model == null)
    {
      throw new DataValidationException(new Dictionary<string, string[]>
      {
        { typeof(T).Name, new[] { $"{typeof(T).Name} cannot be null." } }
      });
    }
    ValidationResult result = await validator.ValidateAsync(model, cancellationToken);
    if (!result.IsValid)
    {
      throw result.ToDataValidationException();
    }
  }

  public static async Task<Dictionary<string, string[]>?> EnsureValidOrToDictonary<T>(
    this IValidator<T> validator,
    T model,
    CancellationToken cancellationToken = default
  )
  {
    ValidationResult result = await validator.ValidateAsync(model, cancellationToken);
    if (!result.IsValid)
      return result.Errors
          .GroupBy(e => e.PropertyName)
          .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

    return null;
  }



  public static DataValidationException ToDataValidationException(this ValidationResult result)
  {
    return new DataValidationException(
      result.Errors
        .GroupBy(e => e.PropertyName)
        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));
  }
}