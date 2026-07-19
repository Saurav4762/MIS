using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;



public interface IDecisionRepo
{
  public Task<Decision> CreateDecision(Decision decision);
  public Task<Decision> UpdateDecision(Guid familyId, Decision decision);
  public Task<Decision?> GetDecisionByFamilyId(Guid familyId);
}