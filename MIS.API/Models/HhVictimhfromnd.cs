using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhVictimhfromnd
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public int? DeathMale { get; set; }
        public int? DeathFemale { get; set; }
        public int? InjuryMale { get; set; }
        public int? InjuryFemale { get; set; }
        public int? MissingMale { get; set; }
        public int? MissingFemale { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
