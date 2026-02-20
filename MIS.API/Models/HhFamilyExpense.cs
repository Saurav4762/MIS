namespace MIS.API.Models
{
    public class HhFamilyExpense
    {
        public Guid HouseholdId { get; set; }
        public decimal? ExpenseFood { get; set; }
        public decimal? ExpenseEducation { get; set; }
        public decimal? ExpenseHealth { get; set; }
        public decimal? ExpenseAgriculture { get; set; }
        public decimal? ExpenseOther { get; set; }
        public string? ExpenseOtherDesc { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
