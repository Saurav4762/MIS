using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Economies;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class EconomyRepo(ApplicationDbContext context) : IEconomyRepo
{
  private readonly ApplicationDbContext _context = context;

  // Implementation for the Economy repository
  public async Task<Economy> CreateEconomyAsync(Economy economy)
  {
    _context.Economics.Add(economy);
    await _context.SaveChangesAsync();
    return economy;
  }

  public async Task<Economy?> GetEconomyByFamilyIdAsync(Guid familyId)
  {
    return await _context.Economics.FirstOrDefaultAsync(e => e.FamilyId == familyId);
  }

  public async Task<Economy> UpdateEconomyAsync(Guid familyId, Economy economy)
  {
    throw new NotImplementedException();
  }
}
