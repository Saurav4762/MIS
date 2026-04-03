using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;
using MIS.Infrastructure.Persistence.Repositories.BaseRepos;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Municipalities;

public class MunicipalityRepo : BaseRepo<Municipality>, IMunicipalityRepo
{
	private readonly ApplicationDbContext _context;

	public MunicipalityRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Municipality> CreateMunicipalityAsync(Municipality municipality)
	{
		await _context.Set<Municipality>().AddAsync(municipality);
		await _context.SaveChangesAsync();
		return municipality;
	}

	public async Task<List<Municipality>> GetAllMunicipalitiesAsync()
	{
		return await _context.Set<Municipality>().ToListAsync();
	}

	public async Task<Municipality?> GetMunicipalityByIdAsync(Guid id)
	{
		return await _context.Set<Municipality>().FirstOrDefaultAsync(x => x.Id == id);
	}
	public async Task<Municipality?> GetByUniqueIdentifiersAsync(string? code, string? phone, string? email)
	{
		return await _context.Municipalities
				.FirstOrDefaultAsync(m => m.Code == code
															|| m.PhoneNo == phone
															|| m.Email == email);
	}

	public async Task<bool> ExistsAsync(string code, string phone, string email)
	{
		return await _context.Municipalities
				.AnyAsync(m => m.Code == code
										|| m.PhoneNo == phone
										|| m.Email == email);
	}

	public async Task<Municipality> UpdateMunicipalityAsync(Municipality municipality)
	{
		var existing = await _context.Set<Municipality>().FirstOrDefaultAsync(x => x.Id == municipality.Id)
			?? throw new NotFoundException(nameof(Municipality), nameof(Municipality.Id), municipality.Id);

		existing.Code = municipality.Code;
		existing.NameEn = municipality.NameEn;
		existing.NameNe = municipality.NameNe;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteMunicipalityAsync(Guid id)
	{
		return await _context.Set<Municipality>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<int> BulkInsertAsync(List<Municipality> entities)
	{
		return await ExecuteAsync<int>(async () =>
		{
			await _context.AddRangeAsync(entities);
			return await _context.SaveChangesAsync();
		});
	}
}
