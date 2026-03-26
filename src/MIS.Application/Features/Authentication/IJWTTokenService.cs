using MIS.Domain.Entities.Identity;

namespace MIS.Application.Features.Authentication;

public interface IJwtTokenService
{
  AuthResultDTO GenerateToken(User user);
}