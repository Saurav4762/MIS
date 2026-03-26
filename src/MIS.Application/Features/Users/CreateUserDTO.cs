namespace MIS.Application.Features.Users;


public record CreateUserDTO
{
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string FullName { get; set; } = null!;
}