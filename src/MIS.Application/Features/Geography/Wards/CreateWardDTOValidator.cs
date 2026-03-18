using FluentValidation;

namespace MIS.Application.Features.Geography.Wards;

public class CreateWardDTOValidator : AbstractValidator<CreateWardDTO>
{
	public CreateWardDTOValidator()
	{
		RuleFor(x => x.MunicipalityId)
			.NotEmpty().WithMessage("Municipality id is required");

		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Ward code is required")
			.MaximumLength(20).WithMessage("Ward code must be at most 20 characters");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Ward name is required")
			.MaximumLength(200).WithMessage("Ward name must be at most 200 characters");
	}
}
