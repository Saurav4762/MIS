namespace MIS.API.Models
{
    public class HhAigoatdetail
    {
        public Guid AigoatdetailId { get; set; }
        public Guid HouseholdId { get; set; }
        public int RowNo { get; set; }
        public string? AiGoatName { get; set; }
        public int? GivenBirthGoat { get; set; }
        public int? GoatAge { get; set; }
        public string? AiGoatSemen { get; set; }
        public DateTime? AiGoatDate { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
