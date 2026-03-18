namespace MIS.Application.Features.Authentication;

public interface IAuthenticationService
{
  public Task<AuthResultDTO> LoginAsync(LoginUserDTO dto);
}