using System.ComponentModel.DataAnnotations;

namespace MIS.API.Models
{
    public class Submission
    {
        [Key]
        public Guid SubmissionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? Username { get; set; }
        public string? Deviceid { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? StartGeopoint { get; set; }
        public DateTime? Today { get; set; }
        public string? PCode { get; set; }
        public string? DCode { get; set; }
        public string? LbCode { get; set; }
        public string? RawSubmissionJson { get; set; }

        // Navigation properties
        public AppUser? CreatedByUser { get; set; }
        public Household? Household { get; set; }
    }
}
