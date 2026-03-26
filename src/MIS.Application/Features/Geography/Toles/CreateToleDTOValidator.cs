using FluentValidation;

namespace MIS.Application.Features.Geography.Toles;

public class CreateToleDTOValidator : AbstractValidator<CreateToleDTO>
{
	public CreateToleDTOValidator()
	{
		RuleFor(x => x.WardId)
			.NotEmpty().WithMessage("Ward id is required");

		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Tole code is required")
			.MaximumLength(20).WithMessage("Tole code must be at most 20 characters");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Tole name is required")
			.MaximumLength(200).WithMessage("Tole name must be at most 200 characters");
	}
}
