using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Authentication;

namespace MIS.API.Features.Authentication;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthenticationService _authenticationService;
  public AuthController(IAuthenticationService authenticationService, IValidator<LoginUserDTO> loginValidator)
  {

    _authenticationService = authenticationService;
  }
  [AllowAnonymous]
  [HttpPost]
  public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
  {
    var data = await _authenticationService.LoginAsync(dto);
    return Ok(data);
  }

}