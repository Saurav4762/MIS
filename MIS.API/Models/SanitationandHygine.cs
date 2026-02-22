// -- Module: sanitationandhygine
// CREATE TABLE IF NOT EXISTS mp.hh_sanitationandhygine (
//     household_id uuid PRIMARY KEY REFERENCES mp.household(household_id) ON DELETE CASCADE,
//     sourceofwater text,
//     sourceofwater_other_text text,
//     type_of_toilet text,
//     type_of_toilet_other_text text,
//     causeofnotoilet text,
//     handwash_facility text,
//     updated_at timestamptz NOT NULL DEFAULT now()
// );

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
