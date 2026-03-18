using FluentValidation;
using MIS.Application.Features.Authentication;
using MIS.Domain.Entities.Identity;
using MIS.Domain.Exceptions;
using MIS.Application.Common.Validations;

namespace MIS.Application.Features.Users;

public class UserService : IUserService
{
  readonly IPasswordHashService _passwordHashService;
  private readonly IValidator<CreateUserDTO> _createUserValidator;
  readonly IUserRepository _repo;
  public UserService(
      IPasswordHashService passwordHashService,
      IUserRepository repo,
      IValidator<CreateUserDTO> createUserValidator
    )
  {
    _passwordHashService = passwordHashService;
    _repo = repo;
    _createUserValidator = createUserValidator;
  }
  public async Task<UserDTO> CreateUserAsync(CreateUserDTO dto)
  {

    await _createUserValidator.EnsureValidOrThrowAsync(dto);


    if (await _repo.ExistingUserByEmailAsync(dto.Email))
    {
      throw new ConflictException(nameof(User), "Email already exits");
    }
    if (await _repo.ExistingUserByUsernameAsync(dto.Email))
    {
      throw new ConflictException(nameof(User), "Username already exits");
    }

    var user = new User
    {
      Id = Guid.NewGuid(),
      CreatedAt = DateTime.UtcNow,
      Email = dto.Email,
      FullName = dto.FullName,
      PasswordHash = _passwordHashService.Hash(dto.Password)
    };
    await _repo.CreateUserAsync(user);
    var userDTO = new UserDTO
    {
      Id = user.Id,
      CreatedAt = user.CreatedAt,
      Email = dto.Email,
      FullName = dto.FullName
    };
    return userDTO;

  }
}