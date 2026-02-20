namespace MIS.API.Models
{
    public class HhAicowdetail
    {
        public Guid AicowdetailId { get; set; }
        public Guid HouseholdId { get; set; }
        public int RowNo { get; set; }
        public string? AiCowName { get; set; }
        public int? GivenBirth { get; set; }
        public int? CowAge { get; set; }
        public string? AiCowSemen { get; set; }
        public DateTime? AiCowDate { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
