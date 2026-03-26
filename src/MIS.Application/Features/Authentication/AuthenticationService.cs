using FluentValidation;
using MIS.Application.Common.Validations;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Authentication;

public class AuthenticationService : IAuthenticationService
{
  readonly IAuthenticationRepository _repo;
  readonly IPasswordHashService _passwordHashService;
  readonly IValidator<LoginUserDTO> _loginValidator;
  readonly IJwtTokenService _jwtTokenService;

  public AuthenticationService(
    IAuthenticationRepository repo,
    IPasswordHashService passwordHashService,
    IValidator<LoginUserDTO> loginValidator,
    IJwtTokenService jwtTokenService
  )
  {
    _repo = repo;
    _passwordHashService = passwordHashService;
    _loginValidator = loginValidator;
    _jwtTokenService = jwtTokenService;
  }

  public async Task<AuthResultDTO> LoginAsync(LoginUserDTO dto)
  {
    await _loginValidator.EnsureValidOrThrowAsync(dto);
    var loginUser = await _repo.GetByEmailAsync(dto.Email) ?? throw new UnauthorizedException("Invalid email or password");

    if (!_passwordHashService.Verify(dto.Password, loginUser.PasswordHash))
    {
      throw new UnauthorizedException("Invalid email or password");
    }
    ;
    return _jwtTokenService.GenerateToken(loginUser);

  }

}