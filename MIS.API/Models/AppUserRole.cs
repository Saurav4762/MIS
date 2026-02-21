using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class AppUserRole
    {
        [Key]
        public Guid UserId { get; set; }
        public string RoleCode { get; set; } = null!;

        // Navigation properties
        public AppUser? User { get; set; }
        public AppRole? Role { get; set; }
    }
}
