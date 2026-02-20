namespace MIS.API.Models
{
    public class HhDisasterInfo
    {
        public Guid HouseholdId { get; set; }
        public string? DisasterExperienced { get; set; }
        public string? DisasterType { get; set; }
        public string? DisasterTypeOtherText { get; set; }
        public int? DisasterYear { get; set; }
        public string? DisasterSupportReceived { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
