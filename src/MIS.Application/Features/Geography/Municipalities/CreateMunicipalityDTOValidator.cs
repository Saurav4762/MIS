using FluentValidation;

namespace MIS.Application.Features.Geography.Municipalities;

public class CreateMunicipalityDTOValidator : AbstractValidator<CreateMunicipalityDTO>
{
	public CreateMunicipalityDTOValidator()
	{
		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Municipality code is required")
			.MaximumLength(20).WithMessage("Municipality code must be at most 20 characters");

		RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage("Municipality English name is required")
			.MaximumLength(200).WithMessage("Municipality English name must be at most 200 characters");

		RuleFor(x => x.NameNe)
			.NotEmpty().WithMessage("Municipality Nepali name is required")
			.MaximumLength(200).WithMessage("Municipality Nepali name must be at most 200 characters");
	}
}
