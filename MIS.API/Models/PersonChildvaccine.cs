namespace MIS.API.Models
{
    public class PersonChildvaccine
    {
        public Guid PersonId { get; set; }
        public string ListName { get; set; } = "childvaccine";
        public string? ChoiceCode { get; set; }
        public string? OtherText { get; set; }

        // Navigation properties
        public Person? Person { get; set; }
    }
}
