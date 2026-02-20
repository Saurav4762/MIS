namespace MIS.API.Models
{
    public class HhAgricultureInformation
    {
        public Guid HouseholdId { get; set; }
        public string? HasAgriculture { get; set; }
        public string? LandOwnership { get; set; }
        public string? LandOwnershipOtherText { get; set; }
        public decimal? TotalLand { get; set; }
        public string? LandUnit { get; set; }
        public decimal? IrrigatedLand { get; set; }
        public decimal? RainfedLand { get; set; }
        public string? HasGreenhouse { get; set; }
        public int? GreenhouseNo { get; set; }
        public string? HasFarmMachinery { get; set; }
        public string? MachineryDetails { get; set; }
        public string? HasAgriInsurance { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
