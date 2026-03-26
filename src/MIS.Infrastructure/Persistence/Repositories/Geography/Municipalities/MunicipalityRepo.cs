using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Municipalities;

public class MunicipalityRepo : IMunicipalityRepo
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
}
