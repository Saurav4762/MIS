using FluentValidation;

namespace MIS.Application.Common.Extensions;

public static class NepaliValidatorExtensions
{
  public static IRuleBuilderOptions<T, string> MustBeNepali<T>(
      this IRuleBuilder<T, string> ruleBuilder, string? errorMessage = null)
  {
    return ruleBuilder.Must(BeNepali)
        .WithMessage(errorMessage ?? "'{PropertyName}' must contain Nepali (Devanagari) text only.");
  }

  public static IRuleBuilderOptions<T, string?> MustBeNepaliOrEmpty<T>(
      this IRuleBuilder<T, string?> ruleBuilder, string? errorMessage = null)
  {
    return ruleBuilder.Must(input => string.IsNullOrWhiteSpace(input) || BeNepali(input))
        .WithMessage(errorMessage ?? "'{PropertyName}' must contain Nepali (Devanagari) text only.");
  }

  private static bool BeNepali(string? input)
  {
    if (string.IsNullOrWhiteSpace(input)) return false;

    return true;
    return input.All(c =>
        char.IsWhiteSpace(c) ||
        (c >= '\u0900' && c <= '\u097F') ||
        c == '।' || c == '?' || c == '!' || c == ',' || c == '.');
  }
}