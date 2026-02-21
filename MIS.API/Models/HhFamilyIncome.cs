using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhFamilyIncome
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }

        public decimal? IncomeAgriculture { get; set; }
        public decimal? IncomeLivestock { get; set; }
        public decimal? IncomeBusiness { get; set; }
        public decimal? IncomeForeign { get; set; }
        public decimal? IncomeJob { get; set; }
        public decimal? IncomeGovt { get; set; }
        public decimal? IncomeOther { get; set; }
        public string? IncomeOtherDesc { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
