using MIS.API.Models;

namespace MIS.API.Interfaces.IRepositories;

public interface IAuthRepo
{
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<AppUser> RegisterUserAsync(AppUser user, string password);
    Task<AppUser> LoginAsync(string username, string password);
}