namespace MIS.API.Models
{
    public class HhAiswinedetail
    {
        public Guid AiswinedetailId { get; set; }
        public Guid HouseholdId { get; set; }
        public int RowNo { get; set; }
        public string? AiSwineName { get; set; }
        public int? GivenBirthSwine { get; set; }
        public int? SwineAge { get; set; }
        public string? AiSwineSemen { get; set; }
        public DateTime? AiSwineDate { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
