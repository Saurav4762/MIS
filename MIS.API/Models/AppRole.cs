using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MIS.API.Models;

public class AppRole
{
    [Key]
    public Guid Id { get; set; }

    public string RoleCode { get; set; } = null!;
    public string RoleName { get; set; } = null!;

    public ICollection<AppUserRole> AppUserRoles { get; set; }
     = new List<AppUserRole>();
}
