using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HouseholdAlterSourceOfLight
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public string ListName { get; set; } = "alt_sourceoflight";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
