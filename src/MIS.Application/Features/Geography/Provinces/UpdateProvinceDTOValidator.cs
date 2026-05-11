using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Provinces;

public class UpdateProvinceDTOValidator : AbstractValidator<UpdateProvinceDTO>
{
	public UpdateProvinceDTOValidator()
	{
		RuleFor(x => x.Code)
			.MaximumLength(20).WithMessage("Province code must be at most 20 characters")
			.When(x => x.Code is not null);

		RuleFor(x => x.NameEn)
			.MaximumLength(200).WithMessage("Province English name must be at most 200 characters")
			.When(x => x.NameEn is not null);

		RuleFor(x => x.NameNe)
			.MustBeNepaliOrEmpty()
      .WithName("Province Nepali name")
			.MaximumLength(200).WithMessage("Province Nepali name must be at most 200 characters")
			.When(x => x.NameNe is not null);
	}
}
