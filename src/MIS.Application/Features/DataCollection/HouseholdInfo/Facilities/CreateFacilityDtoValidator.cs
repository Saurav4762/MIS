using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public class CreateFacilityDtoValidator : AbstractValidator<CreateFacilityDto>
{
    public CreateFacilityDtoValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("FamilyId is required.");

        RuleFor(x => x.DrinkingWaterId)
            .NotEmpty().WithMessage("DrinkingWaterId is required.");

        RuleFor(x => x.ToiletTypeId)
            .NotEmpty().WithMessage("ToiletTypeId is required.");

        RuleFor(x => x.ElectricityId)
            .NotEmpty().WithMessage("ElectricityId is required.");

        RuleFor(x => x.AltLightId)
            .NotEmpty().WithMessage("AltLightId is required.");

        RuleFor(x => x.CookingFuelId)
            .NotEmpty().WithMessage("CookingFuelId is required.");

        RuleFor(x => x.StoveTypeId)
            .NotEmpty().WithMessage("StoveTypeId is required.");
    }
}
