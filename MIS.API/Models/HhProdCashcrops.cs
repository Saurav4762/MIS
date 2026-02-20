namespace MIS.API.Models
{
    public class HhProdCashcrops
    {
        public Guid HouseholdId { get; set; }
        public decimal? Tea { get; set; }
        public decimal? Coffee { get; set; }
        public decimal? Cardamom { get; set; }
        public decimal? BroomGrass { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
