
using MIS.Domain.Entities.Submissions;
namespace MIS.Application.Features.Submissions;


public static class ToSubmissionDTO
{
  public static SubmissionDTO ToDTO(this Submission dto)
  {
    return new SubmissionDTO
    {
      Id = dto.Id,
      Status = dto.Status,
      SubmittedAt = dto.SubmittedAt,
      SubmittedById = dto.SubmittedById,
      RejectionReason = dto.RejectionReason
    };
  }
}