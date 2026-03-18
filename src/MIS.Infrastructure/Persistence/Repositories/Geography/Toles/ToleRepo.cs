using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Geography.Toles;
using MIS.Domain.Entities.Geography;
using MIS.Domain.Exceptions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Geography.Toles;

public class ToleRepo : IToleRepo
{
	private readonly ApplicationDbContext _context;

	public ToleRepo(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Tole> CreateToleAsync(Tole tole)
	{
		await _context.Set<Tole>().AddAsync(tole);
		await _context.SaveChangesAsync();
		return tole;
	}

	public async Task<List<Tole>> GetAllTolesAsync()
	{
		return await _context.Set<Tole>().ToListAsync();
	}

	public async Task<List<Tole>> GetTolesByWardIdAsync(Guid wardId)
	{
		return await _context.Set<Tole>()
			.Where(x => x.WardId == wardId)
			.ToListAsync();
	}

	public async Task<Tole?> GetToleByIdAsync(Guid id)
	{
		return await _context.Set<Tole>().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Tole> UpdateToleAsync(Tole tole)
	{
		var existing = await _context.Set<Tole>().FirstOrDefaultAsync(x => x.Id == tole.Id)
			?? throw new NotFoundException(nameof(Tole), nameof(Tole.Id), tole.Id);

		existing.WardId = tole.WardId;
		existing.Code = tole.Code;
		existing.Name = tole.Name;

		await _context.SaveChangesAsync();
		return existing;
	}

	public async Task<int> DeleteToleAsync(Guid id)
	{
		return await _context.Set<Tole>()
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}
