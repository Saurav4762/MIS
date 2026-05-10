using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Wards;

public class CreateWardDTOValidator : AbstractValidator<CreateWardDTO>
{
	public CreateWardDTOValidator()
	{
		RuleFor(x => x.MunicipalityId)
			.NotEmpty().WithMessage("Municipality id is required");
		RuleFor(x => x.Number)
			.NotEmpty().WithMessage("Ward number is required")
			.GreaterThan(0).WithMessage("Ward number must be greater than 0");
		RuleFor(x => x.RepresentativeNameEn)
			.NotEmpty().WithMessage("Representative name in English is required")
			.MaximumLength(100).WithMessage("Representative name in English must not exceed 100 characters");
		RuleFor(x => x.RepresentativeNameNe)
			.NotEmpty().WithMessage("Representative name in Nepali is required")
			.MustBeNepali()
			.WithName("Representative name in Nepali")
			.MaximumLength(100).WithMessage("Representative name in Nepali must not exceed 100 characters");

		RuleFor(x => x.Email)
			.EmailAddress().WithMessage("Invalid email format")
			.When(x => !string.IsNullOrEmpty(x.Email));
		RuleFor(x => x.PhoneNo)
			.NotEmpty().WithMessage("Phone number cannot be empty")
			.When(x => !string.IsNullOrEmpty(x.PhoneNo));

	}
}
