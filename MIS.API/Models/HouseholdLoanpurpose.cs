using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HouseholdLoanpurpose
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public string ListName { get; set; } = "loanpurpose";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
