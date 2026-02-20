namespace MIS.API.Models
{
    public class AppUser
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
