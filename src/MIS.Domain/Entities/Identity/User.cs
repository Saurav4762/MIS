using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Identity;

public class User : BaseEntity
{
  public string Username { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;
  public bool IsActive { get; set; } = true;
  public DateTime CreatedAt { get; set; }

}