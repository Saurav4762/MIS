using FluentValidation;

namespace MIS.Application.Features.Geography.Municipalities;

public class UpdateMunicipalityDTOValidator : AbstractValidator<UpdateMunicipalityDTO>
{
	public UpdateMunicipalityDTOValidator()
	{
		RuleFor(x => x.Code)
			.MaximumLength(20).WithMessage("Municipality code must be at most 20 characters")
			.When(x => x.Code is not null);

		RuleFor(x => x.NameEn)
			.MaximumLength(200).WithMessage("Municipality English name must be at most 200 characters")
			.When(x => x.NameEn is not null);

		RuleFor(x => x.NameNe)
			.MaximumLength(200).WithMessage("Municipality Nepali name must be at most 200 characters")
			.When(x => x.NameNe is not null);
	}
}
