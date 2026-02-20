namespace MIS.API.Models
{
    public class HouseholdMunIssues
    {
        public Guid HouseholdId { get; set; }
        public string ListName { get; set; } = "mun_issues";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
