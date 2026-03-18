using MIS.Domain.Entities.Identity;

namespace MIS.Application.Features.Authentication;

public interface IAuthenticationRepository
{
  public Task<User?> GetByEmailAsync(string email);
}