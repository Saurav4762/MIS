namespace MIS.API.Models
{
    public class HhAgrovetProduction
    {
        public Guid HouseholdId { get; set; }
        public decimal? UreaKg { get; set; }
        public decimal? DapKg { get; set; }
        public decimal? PotashKg { get; set; }
        public decimal? PesticideLtr { get; set; }
        public decimal? VetMedicineCost { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
