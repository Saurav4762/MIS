namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

public interface IDecisionService
{
  public Task<DecisionDTO> CreateDecision(CreateDecisionDTO createDecisionDTO);
  public Task<DecisionDTO> UpdateDecision(Guid familyId, UpdateDecisionDTO updateDecisionDTO);
  public Task<DecisionDTO> GetDecisionByFamilyId(Guid familyId);
}