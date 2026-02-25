using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace MIS.API.Models;
//  House {
//         Guid Id PK
//         String HouseNumber UK
//         Point Location
//         String HouseType
//         String LandType
//         String RoofType
//         String WallType
//         String Image
//         String Purpose
//         DateTime BuidYear
//         DateTime UpdatedAt
//     }

public class House
{
    [Key]
    public Guid Id { get; set; }
    public Guid ToleId { get; set; }
    public string HouseNumber { get; set; } = null!;
    public Point Location { get; set; } = null!;    
    public string HouseType { get; set; } = null!;
    public string LandType { get; set; } = null!;
    public string RoofType { get; set; } = null!;
    public string WallType { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public DateTime BuildYear { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public Tole Tole { get; set; } = null!;
}
