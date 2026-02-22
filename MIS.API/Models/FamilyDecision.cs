// -- Module: FamilyDecision
// CREATE TABLE IF NOT EXISTS mp.hh_familydecision (
//     household_id uuid PRIMARY KEY REFERENCES mp.household(household_id) ON DELETE CASCADE,
//     decisionmaking text,
//     land_ownership_in_women text,
//     women_in_coop text,
//     women_in_saving_group text,
//     dalit_inclusion text,
//     pwd_inclusion text,
//     updated_at timestamptz NOT NULL DEFAULT now()
// );

using System;
using System.ComponentModel.DataAnnotations;
namespace MIS.API.Models;
public class FamilyDecision
{
    [Key]
    public Guid Id { get; set; } 
    public Guid HouseholdId { get; set; }
    public string DecisionMaking { get; set; }
    public string LandOwnershipInWomen { get; set; }
    public string WomenInCoop { get; set; }
    public string WomenInSavingGroup { get; set; }
    public string DalitInclusion { get; set; }
    public string PwdInclusion { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Household Household { get; set; }
}