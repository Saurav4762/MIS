namespace MIS.API.Models
{
    public class HhFamilydecision
    {
        public Guid HouseholdId { get; set; }
        public string? Decisionmaking { get; set; }
        public string? LandOwnershipInWomen { get; set; }
        public string? WomenInCoop { get; set; }
        public string? WomenInSavingGroup { get; set; }
        public string? DalitInclusion { get; set; }
        public string? PwdInclusion { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
