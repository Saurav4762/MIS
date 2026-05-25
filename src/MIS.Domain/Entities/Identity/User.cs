using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.Geography;

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
    public Guid? MunicipalityId { get; set; }
    public Municipality? Municipality { get; set; }
    public string? Role { get; set; } // 👈 add this
}