namespace MIS.API.Models
{
    public class HouseholdLoansource
    {
        public Guid HouseholdId { get; set; }
        public string ListName { get; set; } = "loansource";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
