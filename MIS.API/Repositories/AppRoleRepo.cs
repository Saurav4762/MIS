using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class AppRoleRepo(AppDbContext context) : IAppRoleRepo
{
    private AppDbContext _context = context;
    
    public Task<AppRole> CreateAsync(AppRole role)
    {
       _context.AppRoles.Add(role);
       _context.SaveChangesAsync();
       return Task.FromResult<AppRole>(role);
    }

    public async Task<AppRole?> GetRoleById(Guid id)
    {
        return await _context.AppRoles
            .FirstOrDefaultAsync(r =>r.Id == id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await _context.AppRoles.FindAsync(id);
        if(role == null) return false;
        
        _context.AppRoles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }
}