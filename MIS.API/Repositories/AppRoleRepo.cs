using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;

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

    public async Task<AppRole> GetRoleByIdAsync(Guid id)
    {
        return await _context.AppRoles
            .FirstOrDefaultAsync(r => r.Id == id) ?? throw new NotFoundException(entity: nameof(AppRole), nameof(AppRole.Id), id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await _context.AppRoles.FindAsync(id) ?? throw new NotFoundException(
            entity: nameof(AppRole),
            key: nameof(AppRole.Id),
            value: id
        );

        _context.AppRoles.Remove(role);
        await _context.SaveChangesAsync();
    }
}