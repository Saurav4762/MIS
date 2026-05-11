using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Districts;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Districts;

public class DistrictRepo : IDistrictRepo
{
	private readonly ApplicationDbContext _context;

	public DistrictRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<District> CreateDistrictAsync(District district)
	{
		await _context.Set<District>().AddAsync(district);
		await _context.SaveChangesAsync();
		return district;
	}

	public async Task<List<District>> GetAllDistrictsAsync()
	{
		return await _context.Set<District>().ToListAsync();
	}

	public async Task<District?> GetDistrictByIdAsync(Guid id)
	{
		return await _context.Set<District>().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<District?> GetByCodeAsync(string code)
	{
		return await _context.Set<District>().FirstOrDefaultAsync(x => x.Code == code);
	}

	public async Task<List<District>> GetDistrictsByProvinceIdAsync(Guid provinceId)
	{
		return await _context.Set<District>()
			.Where(x => x.ProvinceId == provinceId)
			.ToListAsync();
	}

	public async Task<List<District>> SearchDistrictsAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		if (string.IsNullOrWhiteSpace(searchQuery))
			return [];

		var query = _context.Districts.AsQueryable().Select(d => new
		{
			District = d,
			Score =
				(EF.Functions.ILike(d.NameEn, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(d.NameNe, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(d.Code, $"%{searchQuery}%") ? 5 : 0)
		})
		.OrderByDescending(x => x.Score)
		.Select(x => x.District);

		if (!string.IsNullOrEmpty(searchBy))
		{
			switch (searchBy.ToLower())
			{
				case "nameen":
					query = query.Where(d => EF.Functions.ILike(d.NameEn, $"%{searchQuery}%"));
					break;

				case "namene":
					query = query.Where(d => EF.Functions.ILike(d.NameNe, $"%{searchQuery}%"));
					break;

				case "code":
					query = query.Where(d => EF.Functions.ILike(d.Code, $"%{searchQuery}%"));
					break;

				default:
					throw new ArgumentException("Invalid searchBy value");
			}
		}
		else
		{
			query = query.Where(d =>
				EF.Functions.ILike(d.NameEn, $"%{searchQuery}%") ||
				EF.Functions.ILike(d.NameNe, $"%{searchQuery}%") ||
				EF.Functions.ILike(d.Code, $"%{searchQuery}%")
			);
		}

		return await query.Skip((pageNumber - 1) * maxResults).Take(maxResults).ToListAsync();
	}

	public async Task<District> UpdateDistrictAsync(District district)
	{
		var existing = await _context.Set<District>().FirstOrDefaultAsync(x => x.Id == district.Id)
			?? throw new NotFoundException(nameof(District), nameof(District.Id), district.Id);

		existing.ProvinceId = district.ProvinceId;
		existing.Code = district.Code;
		existing.NameEn = district.NameEn;
		existing.NameNe = district.NameNe;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteDistrictAsync(Guid id)
	{
		return await _context.Set<District>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}
