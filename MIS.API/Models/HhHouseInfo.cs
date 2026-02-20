namespace MIS.API.Models
{
    public class HhHouseInfo
    {
        public Guid HouseholdId { get; set; }
        public string? HouseLandType { get; set; }
        public string? HouseLandTypeOtherText { get; set; }
        public string? HouseType { get; set; }
        public string? LatrineInHouse { get; set; }
        public string? RoofType { get; set; }
        public string? RoofTypeOtherText { get; set; }
        public string? WallType { get; set; }
        public string? WallTypeOtherText { get; set; }
        public string? HousePurpose { get; set; }
        public decimal? HouseAge { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
