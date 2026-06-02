using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Areas;

public class CreateAreaDTOValidator : AbstractValidator<CreateAreaDTO>
{
	public CreateAreaDTOValidator()
	{
		RuleFor(x => x.DistrictId)
			.NotEmpty().WithMessage("District is required");

		RuleFor(x => x.Number)
			.NotEmpty().WithMessage("Area number is required")
			.GreaterThan(0).WithMessage("Area number must be greater than 0");


	}
}