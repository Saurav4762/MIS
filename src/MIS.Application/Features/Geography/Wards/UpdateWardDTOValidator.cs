using FluentValidation;

namespace MIS.Application.Features.Geography.Wards;

public class UpdateWardDTOValidator : AbstractValidator<UpdateWardDTO>
{
	public UpdateWardDTOValidator()
	{
		RuleFor(x => x.MunicipalityId)
			.NotEmpty().WithMessage("Municipality id cannot be empty")
			.When(x => x.MunicipalityId.HasValue);
		RuleFor(x => x.Number)
			.NotEmpty().WithMessage("Ward number cannot be empty")
			.GreaterThan(0).WithMessage("Ward number must be greater than 0")
			.When(x => x.Number.HasValue);
		RuleFor(x => x.RepresentativeNameEn)
			.NotEmpty().WithMessage("Representative name in English cannot be empty")
			.MaximumLength(100).WithMessage("Representative name in English must not exceed 100 characters")
			.When(x => x.RepresentativeNameEn is not null);
		RuleFor(x => x.RepresentativeNameNe)
			.NotEmpty().WithMessage("Representative name in Nepali cannot be empty")
			.MaximumLength(100).WithMessage("Representative name in Nepali must not exceed 100 characters")
			.When(x => x.RepresentativeNameNe is not null);


	}
}
