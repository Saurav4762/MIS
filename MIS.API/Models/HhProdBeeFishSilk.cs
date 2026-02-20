namespace MIS.API.Models
{
    public class HhProdBeeFishSilk
    {
        public Guid HouseholdId { get; set; }
        public int? BeeHiveNo { get; set; }
        public decimal? HoneyProductionKg { get; set; }
        public int? FishPondNo { get; set; }
        public decimal? FishProductionKg { get; set; }
        public decimal? SilkProductionKg { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
