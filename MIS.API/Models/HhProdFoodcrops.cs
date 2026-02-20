namespace MIS.API.Models
{
    public class HhProdFoodcrops
    {
        public Guid HouseholdId { get; set; }
        public decimal? Paddy { get; set; }
        public decimal? Maize { get; set; }
        public decimal? Wheat { get; set; }
        public decimal? Millet { get; set; }
        public decimal? Barley { get; set; }
        public decimal? Buckwheat { get; set; }
        public decimal? Potato { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
