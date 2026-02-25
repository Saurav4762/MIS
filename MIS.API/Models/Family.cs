// -- Module: family_info
// CREATE TABLE IF NOT EXISTS mp.hh_family_info (
//     household_id uuid PRIMARY KEY REFERENCES mp.household(household_id) ON DELETE CASCADE,
//     family_type text,
//     total_family_member integer,
//     family_head_name text,
//     family_head_age integer,
//     family_head_gender text,
//     family_head_contact text,
//     has_migrated text,
//     migration_purpose text,
//     migration_purpose_other_text text,
//     migration_district text,
//     migration_district_other_text text,
//     migration_country text,
//     migration_country_other_text text,
//     loan_taken text,
//     loan_amount numeric(14,3),
//     updated_at timestamptz NOT NULL DEFAULT now()
// );

namespace MIS.API.Models;
using System.ComponentModel.DataAnnotations;
public class Family
{
    [Key]
    public Guid Id { get; set; }
}
