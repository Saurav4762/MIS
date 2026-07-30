using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public class UpdateHealthDtoValidator : AbstractValidator<UpdateHealthDto>
{
    public UpdateHealthDtoValidator()
    {
        // If insurance is being explicitly turned on, provider must come along
        When(x => x.HasHealthInsurance == true, () =>
        {
            RuleFor(x => x.InsuranceProviderId)
                .NotEmpty().WithMessage("InsuranceProviderId is required when HasHealthInsurance is true.");
        });

        // If chronic illness list is supplied, it must not be empty
        When(x => x.HasChronicIllness == true, () =>
        {
            RuleFor(x => x.ChronicIllnessIds)
                .NotEmpty().WithMessage("At least one ChronicIllnessId is required when HasChronicIllness is true.");
        });

        When(x => x.ChronicIllnessIds is not null && x.ChronicIllnessIds.Count > 0, () =>
        {
            RuleForEach(x => x.ChronicIllnessIds)
                .NotEmpty().WithMessage("Each ChronicIllnessId must be a valid non-empty Guid.");
        });
    }
}
