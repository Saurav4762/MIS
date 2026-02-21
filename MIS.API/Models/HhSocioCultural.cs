using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhSocioCultural
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public string? Ethnicity { get; set; }
        public string? EthnicityOtherText { get; set; }
        public string? Religion { get; set; }
        public string? ReligionOtherText { get; set; }
        public string? MotherTongue { get; set; }
        public string? MotherTongueOtherText { get; set; }
        public string? ComLanguage { get; set; }
        public string? ComLanguageOtherText { get; set; }
        public string? MajorFamilyOccupation { get; set; }
        public string? MajorFamilyOccupationOtherText { get; set; }
        public string? SettlementType { get; set; }
        public string? FamilyLandArea { get; set; }
        public string? FoodSufficiency { get; set; }
        public int? ChildUnder5 { get; set; }
        public int? Child5to14 { get; set; }
        public int? Youth15to29 { get; set; }
        public int? Adult30to59 { get; set; }
        public int? Senior60plus { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
