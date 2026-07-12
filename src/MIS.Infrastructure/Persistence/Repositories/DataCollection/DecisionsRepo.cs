namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;
using MIS.Infrastructure.Persistence.Data;

public class DecisionsRepo(ApplicationDbContext context) : IDecisionRepo
{
  readonly ApplicationDbContext _context = context;

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