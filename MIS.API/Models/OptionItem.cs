namespace MIS.API.Models
{
    public class OptionItem
    {
        public string ListName { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? LabelEn { get; set; }
        public string? LabelNe { get; set; }
        public string? Extra { get; set; }
        public int? SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public OptionList? List { get; set; }
    }
}
