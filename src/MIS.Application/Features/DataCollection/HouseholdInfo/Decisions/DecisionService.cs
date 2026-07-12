
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public class DecisionService(IDecisionRepo decisionRepo) : IDecisionService
{
  readonly IDecisionRepo _repo = decisionRepo;
  public Task<DecisionDTO> CreateDecision(CreateDecisionDTO createDecisionDTO)
  {
    throw new NotImplementedException();
  }

  public Task<DecisionDTO> GetDecisionByFamilyId(Guid familyId)
  {
    throw new NotImplementedException();
  }

  public Task<DecisionDTO> UpdateDecision(Guid familyId, UpdateDecisionDTO updateDecisionDTO)
  {
    throw new NotImplementedException();
  }
}