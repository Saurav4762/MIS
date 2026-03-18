using FluentValidation;

namespace MIS.Application.Features.Geography.Toles;

public class UpdateToleDTOValidator : AbstractValidator<UpdateToleDTO>
{
	public UpdateToleDTOValidator()
	{
		RuleFor(x => x.WardId)
			.NotEmpty().WithMessage("Ward id cannot be empty")
			.When(x => x.WardId.HasValue);

		RuleFor(x => x.Code)
			.MaximumLength(20).WithMessage("Tole code must be at most 20 characters")
			.When(x => x.Code is not null);

		RuleFor(x => x.Name)
			.MaximumLength(200).WithMessage("Tole name must be at most 200 characters")
			.When(x => x.Name is not null);
	}
}
