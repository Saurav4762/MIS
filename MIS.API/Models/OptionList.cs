using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class OptionList
    {
        [Key]
        public string ListName { get; set; } = null!;
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<OptionItem> OptionItems { get; set; } = new List<OptionItem>();
    }
}
