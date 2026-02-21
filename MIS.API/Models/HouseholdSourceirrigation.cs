using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HouseholdSourceirrigation
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public string ListName { get; set; } = "sourceirrigation";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
