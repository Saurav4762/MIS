using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Areas;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Areas;

public class AreaRepo : IAreaRepo
{
	private readonly ApplicationDbContext _context;

	public AreaRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Area> CreateAreaAsync(Area area)
	{
		await _context.Set<Area>().AddAsync(area);
		await _context.SaveChangesAsync();
		return area;
	}

	public async Task<List<Area>> GetAllAreasAsync()
	{
		return await _context.Set<Area>().ToListAsync();
	}

	public async Task<Area?> GetAreaByIdAsync(Guid id)
	{
		return await _context.Set<Area>().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Area?> GetAreaByNumberAndDistrictIdAsync(int number, Guid districtId)
	{
		return await _context.Set<Area>().FirstOrDefaultAsync(x => x.Number == number && x.DistrictId == districtId);
	}

	public async Task<List<Area>> GetAreasByDistrictIdAsync(Guid districtId)
	{
		return await _context.Set<Area>()
			.Where(x => x.DistrictId == districtId)
			.ToListAsync();
	}


	public async Task<Area> UpdateAreaAsync(Area area)
	{
		var existing = await _context.Set<Area>().FirstOrDefaultAsync(x => x.Id == area.Id)
			?? throw new NotFoundException(nameof(Area), nameof(Area.Id), area.Id);

		existing.DistrictId = area.DistrictId;
		existing.Number = area.Number;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteAreaAsync(Guid id)
	{
		return await _context.Set<Area>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}
