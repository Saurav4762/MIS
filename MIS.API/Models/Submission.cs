using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace MIS.API.Models;
public class Submission
{
    [Key]
    public Guid Id { get; set; }
    public DateTime SubmittedAt { get; set; }
    public Guid SubmittedById { get; set; }
    public string DeviceId { get; set; } = null!;
    public AppUser? SubmittedBy { get; set; }

}