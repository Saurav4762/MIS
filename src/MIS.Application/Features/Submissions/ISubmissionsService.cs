
namespace MIS.Application.Features.Submissions;

public interface ISubmissionsService
{
  Task<SubmissionDTO> CreateAsync(CreateSubmissionDTO request);
  Task<SubmissionDTO> GetByIdAsync(Guid id);
  Task<IEnumerable<SubmissionDTO>> GetAllAsync();
  // Task UpdateAsync(Guid id, UpdateSubmissionRequest request, CancellationToken cancellationToken);
  // Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}