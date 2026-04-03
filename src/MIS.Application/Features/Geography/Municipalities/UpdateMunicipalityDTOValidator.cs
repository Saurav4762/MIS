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

		RuleFor(x => x.HeadExecutiveNameEn)
			.MaximumLength(200).WithMessage("Executive head name must be at most 200 characters")
			.When(x => x.HeadExecutiveNameEn is not null);

		RuleFor(x => x.HeadExecutiveNameNe)
			.MaximumLength(200).WithMessage("Executive head name must be at most 200 characters")
			.When(x => x.HeadExecutiveNameNe is not null);

		RuleFor(x => x.Email)
			.EmailAddress().WithMessage("Invalid email address format")
			.MaximumLength(200).WithMessage("Too large email")
			.When(x => x.Email is not null);

		RuleFor(x => x.PhoneNo)
			.Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")
			.WithMessage("Invalid phone number")
			.MaximumLength(20).WithMessage("Too large input for the given field")
			.When(x => x.PhoneNo is not null);


	}
}
