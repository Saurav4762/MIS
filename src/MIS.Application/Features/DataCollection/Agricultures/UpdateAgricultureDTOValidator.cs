using FluentValidation;

namespace MIS.Application.Features.DataCollection.Agricultures;

public class UpdateAgricultureDTOValidator : AbstractValidator<UpdateAgricultureDTO>
{
    public UpdateAgricultureDTOValidator()
    {
        RuleFor(x => x.LandUnitId)
            .NotEmpty().WithMessage("Land unit is required.")
            .When(x => x.LandUnitId.HasValue)
            .WithMessage("Land unit is required.");

        RuleFor(x => x.OwnershipStatusId)
            .NotEmpty().WithMessage("Ownership status is required.")
            .When(x => x.OwnershipStatusId.HasValue)
            .WithMessage("Ownership status is required.");

        RuleFor(x => x.TotalArea)
            .GreaterThan(0).WithMessage("Total area must be greater then 0.")
            .When(x => x.TotalArea.HasValue)
            .WithMessage("Total area must be greater then 0.");

    }
   
}