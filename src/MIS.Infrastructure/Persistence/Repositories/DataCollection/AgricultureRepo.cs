using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.Agricultures;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class AgricultureRepo : IAgricultureRepo
{
    private readonly ApplicationDbContext _context;
    
    public AgricultureRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Agriculture> CreateAgricultureAsync(Agriculture agriculture)
    {
        await _context.Agricultures.AddAsync(agriculture);
        await _context.SaveChangesAsync();
        return agriculture;
    }

    public async Task<Agriculture?> GetAgricultureByIdAsync(Guid id)
    {
        return await _context.Agricultures
            .Include(a => a.SelectedCrops)
            .Include(a => a.Equipments)
            .Include(a => a.LandTypes)
            .Include(a => a.ProblemsFaced)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Agriculture>> GetAllAgriculturesAsync()
    {
        return await _context.Agricultures
            .Include(a => a.SelectedCrops)
            .Include(a => a.Equipments)
            .Include(a => a.LandTypes)
            .Include(a => a.ProblemsFaced)
            .ToListAsync();
    }

    public async Task<Agriculture> UpdateAgricultureAsync(Agriculture agriculture)
    {
        foreach (var crop in agriculture.SelectedCrops)
            _context.Entry(crop).State = EntityState.Added;
        foreach (var equipment in agriculture.Equipments)
            _context.Entry(equipment).State = EntityState.Added;
        foreach (var landType in agriculture.LandTypes)
            _context.Entry(landType).State = EntityState.Added;
        foreach (var problem in agriculture.ProblemsFaced)
            _context.Entry(problem).State = EntityState.Added;

        await _context.SaveChangesAsync();
        return agriculture;
    }

    public async Task DeleteAgricultureAsync (Guid id)
    {
        var agriculture = await _context.Agricultures.FindAsync(id);
        if (agriculture != null)
        {
            _context.Agricultures.Remove(agriculture);
            await _context.SaveChangesAsync();
        }
    }
}