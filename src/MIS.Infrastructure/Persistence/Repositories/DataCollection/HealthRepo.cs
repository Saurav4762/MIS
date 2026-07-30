using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Healths;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class HealthRepo(ApplicationDbContext context) : IHealthRepo
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Health?> GetHealthByFamilyIdAsync(Guid familyId)
    {
        return await _context.Healths
            .Include(h => h.ChronicIllnesses)
            .FirstOrDefaultAsync(h => h.FamilyId == familyId);
    }

    public async Task<Health> CreateHealthAsync(Health health)
    {
        _context.Healths.Add(health);
        await _context.SaveChangesAsync();
        return health;
    }
}
