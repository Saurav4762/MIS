using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Users;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Exceptions;

namespace MIS.API.Features.Users;


[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly IValidator<CreateUserDTO> _createUserValidator;
  public UsersController(IUserService userService, IValidator<CreateUserDTO> createUserValidator)
  {
    _userService = userService;
    _createUserValidator = createUserValidator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
  {
    var userDTO = await _userService.CreateUserAsync(dto);
    return CreatedAtAction(nameof(GetUserById), new {id = userDTO.Id}, userDTO);
  }

  [HttpGet]
  [Route("{id:guid}")]
  public async Task<IActionResult> GetUserById([FromRoute] Guid id)
  {
    throw new NotImplementedException();
  }



}