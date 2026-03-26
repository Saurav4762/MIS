using FluentValidation;

namespace MIS.Application.Features.Authentication;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
  public LoginUserDTOValidator()
  {
    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required")
        .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
  }
}