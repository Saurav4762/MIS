using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public class CreateLivestockDtoValidator : AbstractValidator<CreateLivestockDto>
{
    public CreateLivestockDtoValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("FamilyId is required.");

        When(x => x.HasLivestockPractice, () =>
        {
            RuleFor(x => x.Animals)
                .NotEmpty().WithMessage("Animals are required when livestock practice is enabled.");

            RuleForEach(x => x.Animals).ChildRules(animal =>
            {
                animal.RuleFor(a => a.AnimalTypeId)
                    .NotEmpty().WithMessage("AnimalTypeId is required for each animal.");

                animal.RuleFor(a => a.Count)
                    .GreaterThanOrEqualTo(0).WithMessage("Count must be zero or greater.");
            });
        });

        When(x => x.HasAIServicePractice, () =>
        {
            RuleFor(x => x.AIServices)
                .NotEmpty().WithMessage("AIServices are required when AI service practice is enabled.");

            RuleForEach(x => x.AIServices).ChildRules(aiService =>
            {
                aiService.RuleFor(a => a.AnimalTypeId)
                    .NotEmpty().WithMessage("AnimalTypeId is required for each AI service.");

                aiService.RuleFor(a => a.AnimalName)
                    .NotEmpty().WithMessage("AnimalName is required.");

                aiService.RuleFor(a => a.AgeYears)
                    .GreaterThanOrEqualTo(0).WithMessage("AgeYears must be zero or greater.");

                aiService.RuleFor(a => a.BirthHistoryId)
                    .NotEmpty().WithMessage("BirthHistoryId is required.");

                aiService.RuleFor(a => a.SemenOrBullName)
                    .NotEmpty().WithMessage("SemenOrBullName is required.");

                aiService.RuleFor(a => a.AiServiceDate)
                    .NotEmpty().WithMessage("AiServiceDate is required.");

                aiService.RuleFor(a => a.StatusId)
                    .NotEmpty().WithMessage("StatusId is required.");
            });
        });
    }
}
