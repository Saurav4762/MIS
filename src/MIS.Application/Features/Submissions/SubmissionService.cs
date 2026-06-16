using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Domain.Entities.Submissions;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.Submissions;



public class SubmissionService : ISubmissionsService
{
  private readonly ISubmissionsRepo _submissionRepo;
  private readonly IValidator<CreateSubmissionDTO> _validator;

  public SubmissionService(ISubmissionsRepo submissionRepo, IValidator<CreateSubmissionDTO> validator)
  {
    _submissionRepo = submissionRepo;
    _validator = validator;
  }

  public async Task<SubmissionDTO> CreateAsync(CreateSubmissionDTO request)
  {
    await _validator.EnsureValidOrThrowAsync(request);
    var submission = new Submission
    {
      Id = Guid.NewGuid(),
      RejectionReason = "",
      SubmittedAt = DateTime.UtcNow,
      SubmittedById = request.SubmittedById,
      Status = SubmissionStatus.Draft
    };

    var res = await _submissionRepo.CreateAsync(submission);

    return res.ToDTO();
  }

  public async Task<SubmissionDTO> GetByIdAsync(Guid id)
  {
    var submission = await _submissionRepo.GetByIdAsync(id);
    return submission?.ToDTO() ?? throw new NotFoundException(nameof(Submission), nameof(Submission.Id), id);
  }

  public async Task<IEnumerable<SubmissionDTO>> GetAllAsync()
  {
    var submissions = await _submissionRepo.GetAllAsync();
    return submissions.Select(x => x.ToDTO());
  }
}