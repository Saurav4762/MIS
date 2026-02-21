using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhProdLivestock
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public int? CowNo { get; set; }
        public int? BuffaloNo { get; set; }
        public int? GoatNo { get; set; }
        public int? PigNo { get; set; }
        public int? PoultryNo { get; set; }
        public decimal? MilkDailyLtr { get; set; }
        public decimal? MeatYearlyKg { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
