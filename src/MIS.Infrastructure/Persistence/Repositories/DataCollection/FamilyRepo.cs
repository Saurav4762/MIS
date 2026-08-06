using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Families;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class FamilyRepo : IFamilyRepo
{
    private readonly ApplicationDbContext _context;

    public FamilyRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task CreateFamilyAsync(Family family)
    {
        _context.Families.Add(family);
        return _context.SaveChangesAsync();
    }

    public async Task<Family?> GetFamilyByIdAsync(Guid id)
    {
        
        // Include members and common 1:1 sub-forms if you want them populated
        var f=  await _context.Families
            .Include(f => f.Members)
            .Include(f => f.Agriculture)
            .Include(f => f.Decision)
            .Include(f => f.Disaster)
            .Include(f => f.Economic)
            .Include(f => f.Facilities)
            .Include(f => f.Health)
            .Include(f => f.Livestock)
            .Include(f => f.Social)
            .Include(f => f.Migrations)
            .FirstOrDefaultAsync(f => f.Id == id);
        
        return f;
    }
}