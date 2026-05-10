using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Provinces;

public class CreateProvinceDTOValidator : AbstractValidator<CreateProvinceDTO>
{
	public CreateProvinceDTOValidator()
	{
		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Province code is required")
			.MaximumLength(20).WithMessage("Province code must be at most 20 characters");

		RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage("Province English name is required")
			.MaximumLength(200).WithMessage("Province English name must be at most 200 characters");

		RuleFor(x => x.NameNe)
			.NotEmpty().WithMessage("Province Nepali name is required")
			.MustBeNepali()
      .WithName("Province Nepali name")
			.MaximumLength(200).WithMessage("Province Nepali name must be at most 200 characters");
	}
}
