using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DataCollection.HouseholdInfo.Socials;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.DataCollection;

public class SocialRepo : ISocialRepo
{
    private readonly ApplicationDbContext _context;

    public SocialRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task CreateSocialAsync(Social social)
    {
        _context.Socials.Add(social);
        return _context.SaveChangesAsync();
    }

    public async Task<Social?> GetSocialByIdAsync(Guid id)
    {
        return await _context.Socials
            .Include(s => s.Family)
            .Include(s => s.Ethnicity)
            .Include(s => s.Religion)
            .Include(s => s.MotherTongue)
            .Include(s => s.CommonLanguage)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}