

using System;
using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models;
public class SanitationandHygine
{
    [Key]
    public Guid Id { get; set; } 
    public Guid HouseholdId { get; set; }
    public Guid SourceOfWaterId { get; set; }
    public OptionItem SourceOfWater { get; set; }=null!;
    public string? SourceOfWaterOtherText { get; set; }
    public Guid TypeOfToiletId { get; set; }

    public OptionItem TypeOfToiletOption { get; set; }= null!;
    public string? TypeOfToiletOtherText { get; set; }
    public string CauseOfNoToilet { get; set; } = null!;
    public Guid HandwashFacilityId { get; set; }
    public OptionItem HandwashFacility { get; set; }= null!;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Household Household { get; set; }= null!;

}
