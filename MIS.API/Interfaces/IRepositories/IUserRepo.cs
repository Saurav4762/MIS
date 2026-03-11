using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IUserRepo
{ 
    Task<IEnumerable<AppUser>> GetAllUsers();
    
    Task<AppUser> GetUsersByIdAsync(Guid id);
    
    Task<AppUser> CreateUserAsync (AppUser user);
    
    Task<AppUser> UpdateUserAsync( Guid id ,AppUser user);
    
    Task<bool> DeleteUserAsync(Guid id);
    
    Task<Object> AssignRoleAsync (Guid userId, Guid roleId);

    Task SaveChangesAsync();


}