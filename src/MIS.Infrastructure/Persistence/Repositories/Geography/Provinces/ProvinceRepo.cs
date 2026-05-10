using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Provinces;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Provinces;

public class ProvinceRepo : IProvinceRepo
{
	private readonly ApplicationDbContext _context;

	public ProvinceRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Province> CreateProvinceAsync(Province province)
	{
		await _context.Set<Province>().AddAsync(province);
		await _context.SaveChangesAsync();
		return province;
	}

	public async Task<List<Province>> GetAllProvincesAsync()
	{
		return await _context.Set<Province>().ToListAsync();
	}

	public async Task<Province?> GetProvinceByIdAsync(Guid id)
	{
		return await _context.Set<Province>().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Province?> GetByCodeAsync(string code)
	{
		return await _context.Set<Province>().FirstOrDefaultAsync(x => x.Code == code);
	}

	public async Task<List<Province>> SearchProvincesAsync(string searchQuery, string? searchBy = null, int maxResults = 10, int pageNumber = 1)
	{
		if (string.IsNullOrWhiteSpace(searchQuery))
			return [];

		var query = _context.Provinces.AsQueryable().Select(p => new
		{
			Province = p,
			Score =
				(EF.Functions.ILike(p.NameEn, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(p.NameNe, $"%{searchQuery}%") ? 10 : 0) +
				(EF.Functions.ILike(p.Code, $"%{searchQuery}%") ? 5 : 0)
		})
		.OrderByDescending(x => x.Score)
		.Select(x => x.Province);

		if (!string.IsNullOrEmpty(searchBy))
		{
			switch (searchBy.ToLower())
			{
				case "nameen":
					query = query.Where(p => EF.Functions.ILike(p.NameEn, $"%{searchQuery}%"));
					break;

				case "namene":
					query = query.Where(p => EF.Functions.ILike(p.NameNe, $"%{searchQuery}%"));
					break;

				case "code":
					query = query.Where(p => EF.Functions.ILike(p.Code, $"%{searchQuery}%"));
					break;

				default:
					throw new ArgumentException("Invalid searchBy value");
			}
		}
		else
		{
			query = query.Where(p =>
				EF.Functions.ILike(p.NameEn, $"%{searchQuery}%") ||
				EF.Functions.ILike(p.NameNe, $"%{searchQuery}%") ||
				EF.Functions.ILike(p.Code, $"%{searchQuery}%")
			);
		}

		return await query.Skip((pageNumber - 1) * maxResults).Take(maxResults).ToListAsync();
	}

	public async Task<Province> UpdateProvinceAsync(Province province)
	{
		var existing = await _context.Set<Province>().FirstOrDefaultAsync(x => x.Id == province.Id)
			?? throw new NotFoundException(nameof(Province), nameof(Province.Id), province.Id);

		existing.Code = province.Code;
		existing.NameEn = province.NameEn;
		existing.NameNe = province.NameNe;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteProvinceAsync(Guid id)
	{
		return await _context.Set<Province>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}
