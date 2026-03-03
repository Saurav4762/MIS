using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        public ICollection<AppUserRole> AppUserRoles { get; set; } 
        = new List<AppUserRole>();

        
    }
}
