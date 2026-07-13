namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

public class DecisionsRepo(ApplicationDbContext context) : IDecisionRepo
{
  readonly ApplicationDbContext _context = context;

  public async Task<Decision> CreateDecision(Decision decision)
  {
    _context.Decisions.Add(decision);
    await _context.SaveChangesAsync();
    return decision;
  }

  public async Task<Decision?> GetDecisionByFamilyId(Guid familyId)
  {
    return await _context.Decisions.FirstOrDefaultAsync(d => d.FamilyId == familyId);
  }

  public async Task<Decision> UpdateDecision(Guid familyId, Decision decision)
  {
    throw new NotImplementedException();
  }
}