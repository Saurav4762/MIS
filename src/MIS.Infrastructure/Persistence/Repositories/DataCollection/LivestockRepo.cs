namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

public class LivestockRepo : ILivestockRepo
{
  private readonly ApplicationDbContext _dbContext;

  public LivestockRepo(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Livestock?> GetLivestockByFamilyIdAsync(Guid familyId)
  {
    return await _dbContext.Livestocks.Include(l => l.Animals)
        .Include(l => l.AIServices)
        .FirstOrDefaultAsync(l => l.FamilyId == familyId);
  }

  public async Task<Livestock> CreateLivestockAsync(Livestock livestock)
  {
    _dbContext.Livestocks.Add(livestock);
    await _dbContext.SaveChangesAsync();
    return livestock;
  }

}