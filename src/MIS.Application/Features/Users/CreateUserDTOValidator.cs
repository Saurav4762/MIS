using FluentValidation;
namespace MIS.Application.Features.Users;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
  public CreateUserDTOValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required")
      .EmailAddress().WithMessage("Invalid email format");

    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required")
      .MinimumLength(6).WithMessage("Password should be atleast 6 digit long");

    RuleFor(x => x.Username)
      .NotEmpty().WithMessage("Username is required")
      .MinimumLength(3).WithMessage("User name must atleast be 3 length");
    RuleFor(x => x.Phone)
      .NotEmpty().WithMessage("Phone number is required")
      .Matches(@"^\+?[0-9]{10,15}$")
      .WithMessage("Invalid phone number");

  }
}