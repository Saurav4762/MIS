using FluentValidation;

namespace MIS.Application.Features.Geography.Wards;

public class UpdateWardDTOValidator : AbstractValidator<UpdateWardDTO>
{
	public UpdateWardDTOValidator()
	{
		RuleFor(x => x.MunicipalityId)
			.NotEmpty().WithMessage("Municipality id cannot be empty")
			.When(x => x.MunicipalityId.HasValue);

		RuleFor(x => x.Code)
			.MaximumLength(20).WithMessage("Ward code must be at most 20 characters")
			.When(x => x.Code is not null);

		RuleFor(x => x.Name)
			.MaximumLength(200).WithMessage("Ward name must be at most 200 characters")
			.When(x => x.Name is not null);
	}
}
