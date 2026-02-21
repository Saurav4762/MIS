using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhProdPulsecrops
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public decimal? Lentil { get; set; }
        public decimal? Chickpea { get; set; }
        public decimal? Pigeonpea { get; set; }
        public decimal? Soybean { get; set; }
        public decimal? Blackgram { get; set; }
        public decimal? Cowpea { get; set; }
        public decimal? Peas { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
