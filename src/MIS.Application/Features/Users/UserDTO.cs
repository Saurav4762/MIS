namespace MIS.Application.Features.Users;

public record UserDTO
{
  public Guid Id { get; set; }
  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public DateTime CreatedAt { get; set; }

}