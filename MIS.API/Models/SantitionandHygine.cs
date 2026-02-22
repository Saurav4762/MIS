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
    public string SourceOfWater { get; set; }
    public string SourceOfWaterOtherText { get; set; }
    public string TypeOfToilet { get; set; }
    public string TypeOfToiletOtherText { get; set; }
    public string CauseOfNoToilet { get; set; }
    public string HandwashFacility { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Household Household { get; set; }
}
