// -- Module: house_info
// CREATE TABLE IF NOT EXISTS mp.hh_house_info (
//     household_id uuid PRIMARY KEY REFERENCES mp.household(household_id) ON DELETE CASCADE,
//     house_land_type text,
//     house_land_type_other_text text,***
//     house_type text,
//     latrine_in_house text,
//     roof_type text,
//     roof_type_other_text text,
//     wall_type text,
//     wall_type_other_text text,
//     house_purpose text,***
//     house_age numeric(14,3),
//     updated_at timestamptz NOT NULL DEFAULT now()
// );

using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace MIS.API.Models;

public class HouseInfo
{
    [Key]
    public Guid Id { get; set; }
    public Guid ToleId { get; set; }
    public string HouseLandType { get; set; } = null!;
    public string HouseLandTypeOtherText { get; set; } = null!;
    public string HouseType { get; set; } = null!;
    public string LatrineInHouse { get; set; } = null!;
    public string RoofType { get; set; } = null!;
    public string RoofTypeOtherText { get; set; } = null!;
    public string WallType { get; set; } = null!;
    public string WallTypeOtherText { get; set; } = null!;
    public string HousePurpose { get; set; } = null!;
    public decimal HouseAge { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Point Coords { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    // Navigation property
    public Tole Tole { get; set; } = null!;
    public IEnumerable<Household>? Households { get; set; } = null;
}