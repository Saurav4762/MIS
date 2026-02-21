using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhFamilyFacilities
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public string? Sourceoflight { get; set; }
        public string? SourceoflightOtherText { get; set; }
        public string? HasInternet { get; set; }
        public string? HasTv { get; set; }
        public string? HasRadio { get; set; }
        public string? HasMobile { get; set; }
        public string? HasSmartphone { get; set; }
        public string? HasComputer { get; set; }
        public string? HasFridge { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
