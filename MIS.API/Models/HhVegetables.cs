using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhVegetables
    {
        [Key]
        public Guid HouseholdId { get; set; }
        public decimal? Cauliflower { get; set; }
        public decimal? Cabbage { get; set; }
        public decimal? Tomato { get; set; }
        public decimal? Onion { get; set; }
        public decimal? Garlic { get; set; }
        public decimal? Ginger { get; set; }
        public decimal? Turmeric { get; set; }
        public decimal? Radish { get; set; }
        public decimal? MustardGreen { get; set; }
        public decimal? Pumpkin { get; set; }
        public decimal? Cucumber { get; set; }
        public decimal? BitterGourd { get; set; }
        public decimal? Squash { get; set; }
        public decimal? Chili { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
