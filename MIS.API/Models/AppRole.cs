namespace MIS.API.Models
{
    public class AppRole
    {
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        // Navigation properties
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}
