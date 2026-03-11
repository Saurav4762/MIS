using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Exceptions;
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

    public async Task<AppUser> GetUsersByIdAsync(Guid id)
    {
        var user = await _userRepo.AppUsers.FindAsync(id) ??
                   throw new NotFoundException(
                       entity: nameof(AppUser),
                       key: nameof(AppUser.Id),
                       value: id);

        return user;

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

    public async Task<Object> AssignRoleAsync(Guid userId, Guid roleId)
    {
       var user = await _userRepo.AppUsers
           .Include(u=>u.AppUserRoles)
           .FirstOrDefaultAsync(u=>u.Id == userId);
       
       if (user == null)
           throw new Exception("User not found");
       
       var role = await _userRepo.AppRoles
           .FirstOrDefaultAsync(r => r.Id == roleId);
       
       if (role == null)
           throw new Exception("Role not found");
       
       if (user.AppUserRoles.Any(ur=>ur.AppRoleId == role.Id))
           throw new Exception("Role already assigned");

       var userrole = new AppUserRole
       {
           AppRoleId = role.Id,
           AppUserId = userId
       };
       
       await _userRepo.AppUserRoles.AddAsync(userrole);
       await _userRepo.SaveChangesAsync();

       return new
       {
           message = "Role assigned successfully",
           userId = user.Id,
           RoleName = role.RoleName
       };

    }

    public async Task SaveChangesAsync()
    {
        await _userRepo.SaveChangesAsync();
    }
}