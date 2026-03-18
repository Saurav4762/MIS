namespace MIS.Application.Features.Users;


public interface IUserService
{
  public Task<UserDTO> CreateUserAsync(CreateUserDTO create);
}