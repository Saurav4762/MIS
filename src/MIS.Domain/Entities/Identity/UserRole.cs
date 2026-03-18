using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.Identity;

public class UserRole : BaseEntity
{
  public Guid AppRoleId { get; set; }
  public Guid AppUserId { get; set; }


  // Navigation properties
  public User AppUser { get; set; } = null!;
  public Role AppRole { get; set; } = null!;

}
