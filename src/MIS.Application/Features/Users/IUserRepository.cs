using MIS.Domain.Entities.Identity;

namespace MIS.Application.Features.Users;


public interface IUserRepository
{
  public Task<bool> ExistingUserByEmailAsync(string email);
  public Task<bool> ExistingUserByUsernameAsync(string username);
  public Task<User> CreateUserAsync(User user);
}