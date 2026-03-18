using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Wards;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Wards;

public class WardRepo : IWarrdRepo
{
	private readonly ApplicationDbContext _context;

	public WardRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Ward> CreateWardAsync(Ward ward)
	{
		await _context.Set<Ward>().AddAsync(ward);
		await _context.SaveChangesAsync();
		return ward;
	}

	public async Task<List<Ward>> GetAllWardsAsync()
	{
		return await _context.Set<Ward>().ToListAsync();
	}

	public async Task<List<Ward>> GetWardsByMunicipalityIdAsync(Guid municipalityId)
	{
		return await _context.Set<Ward>()
			.Where(x => x.MunicipalityId == municipalityId)
			.ToListAsync();
	}

	public async Task<Ward?> GetWardByIdAsync(Guid id)
	{
		return await _context.Set<Ward>().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Ward> UpdateWardAsync(Ward ward)
	{
		var existing = await _context.Set<Ward>().FirstOrDefaultAsync(x => x.Id == ward.Id)
			?? throw new NotFoundException(nameof(Ward), nameof(Ward.Id), ward.Id);

		existing.MunicipalityId = ward.MunicipalityId;
		existing.Code = ward.Code;
		existing.Name = ward.Name;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteWardAsync(Guid id)
	{
		return await _context.Set<Ward>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}
