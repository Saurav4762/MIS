using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Districts;

public class CreateDistrictDTOValidator : AbstractValidator<CreateDistrictDTO>
{
	public CreateDistrictDTOValidator()
	{
		RuleFor(x => x.ProvinceId)
			.NotEmpty().WithMessage("Province is required");

		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("District code is required")
			.MaximumLength(20).WithMessage("District code must be at most 20 characters");

		RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage("District English name is required")
			.MaximumLength(200).WithMessage("District English name must be at most 200 characters");

		RuleFor(x => x.NameNe)
			.NotEmpty().WithMessage("District Nepali name is required")
			.MustBeNepali()
      .WithName("District Nepali name")
			.MaximumLength(200).WithMessage("District Nepali name must be at most 200 characters");
	}
}
