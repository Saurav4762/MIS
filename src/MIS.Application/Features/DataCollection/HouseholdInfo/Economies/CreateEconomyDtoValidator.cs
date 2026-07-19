using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Economies;


// A validator class for the CreateEconomyDto

public class CreateEconomyDtoValidator : AbstractValidator<CreateEconomyDto>
{
  public CreateEconomyDtoValidator()
  {
    RuleFor(x => x.FamilyId)
        .NotEmpty().WithMessage("FamilyId is required.");
    RuleFor(x => x.ClassificationStatusId)
        .NotEmpty().WithMessage("ClassificationStatusId is required.");
    RuleFor(x => x.MainIncomeSourceId)
        .NotEmpty().WithMessage("MainIncomeSourceId is required.");

    // section 02: Annual Expenditure (NPR) should be non-negative
    RuleFor(x => x.FoodExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("FoodExpenditure must be non-negative.");
    RuleFor(x => x.EducationExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("EducationExpenditure must be non -negative.");
    RuleFor(x => x.HealthExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("HealthExpenditure must be non-negative.");
    RuleFor(x => x.ClothingExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("ClothingExpenditure must be non-negative.");
    RuleFor(x => x.AgricultureExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("AgricultureExpenditure must be non-negative.");
    RuleFor(x => x.OtherExpenditure)
        .GreaterThanOrEqualTo(0).WithMessage("OtherExpenditure must be non-negative.");

  }
}