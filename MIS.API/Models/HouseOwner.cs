using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class HouseOwner
{
    [Key]
    public Guid HouseId { get; set; }
    public Guid personId { get; set; }
    public Guid InstituteId { get; set; }
    public string other { get; set; } = string.Empty;
}