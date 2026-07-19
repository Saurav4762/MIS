using FluentValidation;
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public class UpdateDecisionDTOValidator : AbstractValidator<UpdateDecisionDTO>
{
  public UpdateDecisionDTOValidator()
  {
    RuleFor(x => x.FamilyId).NotEmpty().WithMessage("FamilyId is required.");
    RuleFor(x => x.HouseholdExpenseId).NotEmpty().WithMessage("HouseholdExpenseId is required.");
    RuleFor(x => x.PropertyId).NotEmpty().WithMessage("PropertyId is required.");
    RuleFor(x => x.HealthCareId).NotEmpty().WithMessage("HealthCareId is required.");
    RuleFor(x => x.EducationId).NotEmpty().WithMessage("EducationId is required.");
    RuleFor(x => x.InvestmentsId).NotEmpty().WithMessage("InvestmentsId is required.");
    RuleFor(x => x.GovernanceId).NotEmpty().WithMessage("GovernanceId is required.");
  }
};