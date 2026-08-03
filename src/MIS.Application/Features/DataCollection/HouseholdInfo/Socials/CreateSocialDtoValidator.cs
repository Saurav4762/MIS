using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public class CreateSocialDtoValidator : AbstractValidator<CreateSocialDto>

{
    public CreateSocialDtoValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("FamilyId is required.");
        
        RuleFor(x=> x.EthnicityId)
            .NotEmpty().WithMessage("Ethnicity is required");

        RuleFor(x => x.ReligionId)
            .NotEmpty().WithMessage("Religion is required");

        RuleFor(x => x.MotherTongueId)
            .NotEmpty().WithMessage("Mother tongue is required");

        RuleFor(x => x.CommonLanguageId)
            .NotEmpty().WithMessage("Common language is required"); 
    }
    
}