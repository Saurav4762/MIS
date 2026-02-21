using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhAiLivestock
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public string? HasAiService { get; set; }
        public string? AiServiceProvider { get; set; }
        public string? AiServiceProviderOtherText { get; set; }
        public int? AiFrequency { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
