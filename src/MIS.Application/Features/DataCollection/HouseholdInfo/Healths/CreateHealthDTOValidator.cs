using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public class CreateHealthDTOValidator : AbstractValidator<CreateHealthDto>
{
    public CreateHealthDTOValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("FamilyId is required.");

        // InsuranceProviderId is required when health insurance is enabled
        When(x => x.HasHealthInsurance, () =>
        {
            RuleFor(x => x.InsuranceProviderId)
                .NotEmpty().WithMessage("InsuranceProviderId is required when HasHealthInsurance is true.");
        });

        // At least one chronic illness must be selected when the toggle is on
        When(x => x.HasChronicIllness, () =>
        {
            RuleFor(x => x.ChronicIllnessIds)
                .NotEmpty().WithMessage("At least one ChronicIllnessId is required when HasChronicIllness is true.");

            RuleForEach(x => x.ChronicIllnessIds)
                .NotEmpty().WithMessage("Each ChronicIllnessId must be a valid non-empty Guid.");
        });
    }
}
