using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhProdOilcrops
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public decimal? Mustard { get; set; }
        public decimal? Sesame { get; set; }
        public decimal? Sunflower { get; set; }
        public decimal? Groundnut { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
