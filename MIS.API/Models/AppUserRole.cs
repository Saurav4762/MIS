using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class AppUserRole
{
    [Key]
    public Guid AppRoleId { get; set; }
    public Guid AppUserId { get; set; }

    // Navigation properties
    public AppUser? AppUser { get; set; }
    public AppRole? AppRole { get; set; }
    

}
