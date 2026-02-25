using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;

public class HouseOwner
{
    [Key]
    public Guid HouseId { get; set; }
    public Guid OwnerId { get; set; }
    public Guid InstituteId { get; set; }
    public string Other { get; set; } = string.Empty;

    // Navigation Property
    public House House { get; set; } = null!;
    public Person? Person { get; set; } = null;
    public Institute? Institute { get; set; } = null; 
}