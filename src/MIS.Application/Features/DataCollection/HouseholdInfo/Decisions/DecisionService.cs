using MIS.Domain.Entities.DataCollection.HouseholdInfo;
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public class DecisionService(IDecisionRepo decisionRepo) : IDecisionService
{
  readonly IDecisionRepo _repo = decisionRepo;
  public async Task<DecisionDTO> CreateDecision(CreateDecisionDTO createDecisionDTO)
  {
    var decision = new Decision
    {
      FamilyId = createDecisionDTO.FamilyId,
      EducationId = createDecisionDTO.EducationId,
      GovernanceId = createDecisionDTO.GovernanceId,
      HealthCareId = createDecisionDTO.HealthCareId,
      InvestmentsId = createDecisionDTO.InvestmentsId,
      PropertyId = createDecisionDTO.PropertyId,
    };
    return (await _repo.CreateDecision(decision)).MapToDecisionDTO();
  }

  public async Task<DecisionDTO> GetDecisionByFamilyId(Guid familyId)
  {
    var decision = await _repo.GetDecisionByFamilyId(familyId);
    return decision?.MapToDecisionDTO() ?? throw new InvalidOperationException("Decision not found");
  }

  public async Task<DecisionDTO> UpdateDecision(Guid familyId, UpdateDecisionDTO updateDecisionDTO)
  {
    throw new NotImplementedException();
  }
}