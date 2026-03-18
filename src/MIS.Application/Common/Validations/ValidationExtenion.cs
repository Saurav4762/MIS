using FluentValidation;
using FluentValidation.Results;
using MIS.Domain.Exceptions;

namespace MIS.Application.Common.Validations;

public static class ValidationExtensions
{
  public static async Task EnsureValidOrThrowAsync<T>(
    this IValidator<T> validator,
    T model,
    CancellationToken cancellationToken = default)
  {
    ValidationResult result = await validator.ValidateAsync(model, cancellationToken);
    if (!result.IsValid)
    {
      throw result.ToDataValidationException();
    }
  }

  public static DataValidationException ToDataValidationException(this ValidationResult result)
  {
    return new DataValidationException(
      result.Errors
        .GroupBy(e => e.PropertyName)
        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));
  }
}