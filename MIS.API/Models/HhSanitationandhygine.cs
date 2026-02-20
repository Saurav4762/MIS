namespace MIS.API.Models
{
    public class HhSanitationandhygine
    {
        public Guid HouseholdId { get; set; }
        public string? Sourceofwater { get; set; }
        public string? SourceofwaterOtherText { get; set; }
        public string? TypeOfToilet { get; set; }
        public string? TypeOfToiletOtherText { get; set; }
        public string? Causeofnotoilet { get; set; }
        public string? HandwashFacility { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
