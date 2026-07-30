using FluentValidation;
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public class CreateDecisionDTOValidator : AbstractValidator<CreateDecisionDTO>
{
  public CreateDecisionDTOValidator()
  {
    RuleFor(x => x.FamilyId)
      .NotEmpty().WithMessage("Family is required");

    RuleFor(x => x.HouseholdExpenseId)
      .NotEmpty().WithMessage("Household Expense is required");

    RuleFor(x => x.PropertyId)
      .NotEmpty().WithMessage("Property is required");

    RuleFor(x => x.HealthCareId)
      .NotEmpty().WithMessage("Health Care is required");

    RuleFor(x => x.EducationId)
      .NotEmpty().WithMessage("Education is required");

    RuleFor(x => x.InvestmentsId)
      .NotEmpty().WithMessage("Investments is required");

    RuleFor(x => x.GovernanceId)
      .NotEmpty().WithMessage("Governance is required");


  }
}