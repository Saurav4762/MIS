using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Municipalities;

public class ExcelMunicipalityDTOValidator : AbstractValidator<ExcelMunicipalityDTO>
{
	public ExcelMunicipalityDTOValidator()
	{
		RuleFor(x => x.RowNumber)
			.GreaterThan(1).WithMessage("Invalid excel row number");

		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Municipality code is required")
			.MaximumLength(20).WithMessage("Municipality code must be at most 20 characters");

		RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage("Municipality English name is required")
			.MaximumLength(200).WithMessage("Municipality English name must be at most 200 characters");

		RuleFor(x => x.NameNe)
			.NotEmpty().WithMessage("Municipality Nepali name is required")
			.MustBeNepali()
			.WithName("Municipality Nepali name")
			.MaximumLength(200).WithMessage("Municipality Nepali name must be at most 200 characters");

		RuleFor(x => x.HeadExecutiveNameEn)
			.NotEmpty().WithMessage("Executive head english name is required")
			.MaximumLength(200).WithMessage("Executive head name must be at most 200 characters");

		RuleFor(x => x.HeadExecutiveNameNe)
			.NotEmpty().WithMessage("Executive head nepali name is required")
			.MustBeNepali()
			.WithName("Executive head Nepali name")
			.MaximumLength(200).WithMessage("Executive head name must be at most 200 characters");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Municipality email address is required")
			.EmailAddress().WithMessage("Invalid email address format")
			.MaximumLength(200).WithMessage("Too large email");

		RuleFor(x => x.PhoneNo)
			.NotEmpty().WithMessage("Municipality Phone number is required")
			.Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")
			.WithMessage("Invalid phone number")
			.MaximumLength(20).WithMessage("Too large input for the given field");

		RuleFor(x => x.Website)
			.NotEmpty().WithMessage("Municipality website is required")
			.Matches(@"^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:\/?#[\]@!$&'()*+,;=]*)?$")
			.WithMessage("Invalid website URL format");
	}
}
