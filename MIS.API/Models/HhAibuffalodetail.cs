namespace MIS.API.Models
{
    public class HhAibuffalodetail
    {
        public Guid AibuffalodetailId { get; set; }
        public Guid HouseholdId { get; set; }
        public int RowNo { get; set; }
        public string? AiBuffaloName { get; set; }
        public int? GivenBirthBuffalo { get; set; }
        public int? BuffaloAge { get; set; }
        public string? AiBuffaloSemen { get; set; }
        public DateTime? AiBuffaloDate { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
