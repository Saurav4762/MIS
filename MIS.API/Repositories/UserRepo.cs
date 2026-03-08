using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;

namespace MIS.API.Repositories;

public class UserRepo(AppDbContext context) : IUserRepo
{

    private readonly AppDbContext _userRepo = context;
    
    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _userRepo.AppUsers.ToListAsync();
    }

    public async Task<AppUser?> GetUsersByIdAsync(Guid id)
    {
        return await _userRepo.AppUsers
            .Include(u=> u.AppUserRoles)
            .ThenInclude(ur =>ur.AppRole)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUser> CreateUserAsync(AppUser user)
    {
        _userRepo.AppUsers.Add(user);
        await _userRepo.SaveChangesAsync();
        return user;
    }

    public async Task<AppUser> UpdateUserAsync(Guid id, AppUser user)
    {
        var existing = await _userRepo.AppUsers.FindAsync(id);
        
        if (existing == null) 
            
            existing.FullName = user.FullName;
        existing.Email = user.Email;
        existing.Phone = user.Phone;
        existing.IsActive = user.IsActive;
        
        await _userRepo.SaveChangesAsync();
        return existing;
        
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userRepo.AppUsers.FindAsync(id);
        
        if(user == null) return false;
        
        _userRepo.AppUsers.Remove(user);
        await _userRepo.SaveChangesAsync();
        
        return true;
    }

    public async Task AssignRoleAsync(AppUserRole userRole)
    {
        await _userRepo.AppUserRoles.AddAsync(userRole);
    }

    public async Task SaveChangesAsync()
    {
        await _userRepo.SaveChangesAsync();
    }
}