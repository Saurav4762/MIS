using MIS.API.Models;

namespace MIS.API.Repositories.Interfaces;

public interface IAuthRepo
{
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<AppUser> RegisterUserAsync(AppUser user, string password);
    Task<AppUser> LoginAsync(string username, string password);
}