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

	public async Task<Area?> GetByCodeAsync(string code)
	{
		return await _context.Set<Area>().FirstOrDefaultAsync(x => x.Code == code);
	}

	public async Task<List<Area>> GetAreasByDistrictIdAsync(Guid districtId)
	{
		return await _context.Set<Area>()
			.Where(x => x.DistrictId == districtId)
			.ToListAsync();
	}

	public async Task<List<Area>> SearchAreasAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		if (string.IsNullOrWhiteSpace(searchQuery))
			return [];

		var query = _context.Set<Area>().AsQueryable().Select(a => new
		{
			Area = a,
			Score =
				(EF.Functions.ILike(a.NameEn, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(a.NameNe, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(a.Code, $"%{searchQuery}%") ? 5 : 0)
		})
		.OrderByDescending(x => x.Score)
		.Select(x => x.Area);

		if (!string.IsNullOrEmpty(searchBy))
		{
			switch (searchBy.ToLower())
			{
				case "nameen":
					query = query.Where(a => EF.Functions.ILike(a.NameEn, $"%{searchQuery}%"));
					break;

				case "namene":
					query = query.Where(a => EF.Functions.ILike(a.NameNe, $"%{searchQuery}%"));
					break;

				case "code":
					query = query.Where(a => EF.Functions.ILike(a.Code, $"%{searchQuery}%"));
					break;

				default:
					throw new ArgumentException("Invalid searchBy value");
			}
		}
		else
		{
			query = query.Where(a =>
				EF.Functions.ILike(a.NameEn, $"%{searchQuery}%") ||
				EF.Functions.ILike(a.NameNe, $"%{searchQuery}%") ||
				EF.Functions.ILike(a.Code, $"%{searchQuery}%")
			);
		}

		return await query.Skip((pageNumber - 1) * maxResults).Take(maxResults).ToListAsync();
	}

	public async Task<Area> UpdateAreaAsync(Area area)
	{
		var existing = await _context.Set<Area>().FirstOrDefaultAsync(x => x.Id == area.Id)
			?? throw new NotFoundException(nameof(Area), nameof(Area.Id), area.Id);

		existing.DistrictId = area.DistrictId;
		existing.Code = area.Code;
		existing.NameEn = area.NameEn;
		existing.NameNe = area.NameNe;

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
