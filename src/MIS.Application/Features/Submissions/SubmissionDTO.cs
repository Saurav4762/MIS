
namespace MIS.Application.Features.Submissions;
public class SubmissionDTO
{
  public Guid Id { get; set; }
  public SubmissionStatus Status { get; set; } = SubmissionStatus.Draft;
  public DateTime SubmittedAt { get; set; }
  public Guid SubmittedById { get; set; }
  public string? RejectionReason { get; set; }
}
