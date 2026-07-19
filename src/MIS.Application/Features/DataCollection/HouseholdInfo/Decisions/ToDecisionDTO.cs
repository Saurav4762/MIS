using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public static class ToDecisionDTO
{
  public static DecisionDTO MapToDecisionDTO(this Decision decision)
  {
    return new DecisionDTO
    {
      FamilyId = decision.FamilyId,
      EducationId = decision.EducationId,
      GovernanceId = decision.GovernanceId,
      HealthCareId = decision.HealthCareId,
      InvestmentsId = decision.InvestmentsId,
      PropertyId = decision.PropertyId,
    };
  }
}