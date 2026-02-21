using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    }
}
