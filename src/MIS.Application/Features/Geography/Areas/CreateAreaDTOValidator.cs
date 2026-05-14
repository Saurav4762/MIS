using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Areas;

public class CreateAreaDTOValidator : AbstractValidator<CreateAreaDTO>
{
	public CreateAreaDTOValidator()
	{
		RuleFor(x => x.DistrictId)
			.NotEmpty().WithMessage("District is required");

		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Area code is required")
			.MaximumLength(20).WithMessage("Area code must be at most 20 characters");

		RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage("Area English name is required")
			.MaximumLength(200).WithMessage("Area English name must be at most 200 characters");

		RuleFor(x => x.NameNe)
			.NotEmpty().WithMessage("Area Nepali name is required")
			.MustBeNepali()
      .WithName("Area Nepali name")
			.MaximumLength(200).WithMessage("Area Nepali name must be at most 200 characters");
	}
}