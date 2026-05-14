using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.Identity;

namespace MIS.Domain.Entities.Submissions;

public class Submission : BaseEntity
{
	public SubmissionStatus Status { get; set; } = SubmissionStatus.Draft;
	public DateTime SubmittedAt { get; set; }
	public Guid SubmittedById { get; set; }
	public DateTime? ReviewedAt { get; set; }
	public Guid? ReviewedById { get; set; }
	public string? RejectionReason { get; set; }

	// Navigation properties
	public User SubmittedBy { get; set; } = null!;
	public User? ReviewedBy { get; set; }
}
