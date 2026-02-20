namespace MIS.API.Models
{
    public class HhVictimphyfromnd
    {
        public Guid HouseholdId { get; set; }
        public int? HouseDestroyedNo { get; set; }
        public int? LivestockLostNo { get; set; }
        public decimal? LandDamageRopani { get; set; }
        public decimal? CropDamageValue { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
