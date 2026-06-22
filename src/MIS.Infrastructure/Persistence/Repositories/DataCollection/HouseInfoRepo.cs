using MIS.Application.Features.DataCollection.HouseInfo;
using MIS.Domain.Entities.DataCollection.HouseInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class HouseInfoRepo : IHouseInfoRepo
{
  private readonly ApplicationDbContext _context;
  public HouseInfoRepo(ApplicationDbContext context)
  {
    _context = context;
  }

  public Task CreateHouseAsync(House house)
  {
    _context.Houses.Add(house);
    return _context.SaveChangesAsync();
  }

  public async Task<House?> GetHouseByIdAsync(Guid id)
  {
    return await _context.Houses.FindAsync(id);
  }
}