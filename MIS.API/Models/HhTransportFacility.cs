namespace MIS.API.Models
{
    public class HhTransportFacility
    {
        public Guid HouseholdId { get; set; }
        public string? RoadAccess { get; set; }
        public decimal? RoadDistanceKm { get; set; }
        public decimal? NearestMarketDistanceKm { get; set; }
        public decimal? NearestHealthpostDistanceKm { get; set; }
        public decimal? NearestSchoolDistanceKm { get; set; }
        public string? TransportMode { get; set; }
        public decimal? TransportCost { get; set; }
        public string? HasVehicle { get; set; }
        public string? VehicleType { get; set; }
        public string? VehicleTypeOtherText { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
