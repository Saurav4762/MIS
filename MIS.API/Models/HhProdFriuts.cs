using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class HhProdFriuts
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public decimal? Orange { get; set; }
        public decimal? Banana { get; set; }
        public decimal? Mango { get; set; }
        public decimal? Litchi { get; set; }
        public decimal? Lemon { get; set; }
        public decimal? Guava { get; set; }
        public decimal? Pineapple { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
