using MIS.Domain.Entities.Submissions;

namespace MIS.Application.Features.Submissions;


public interface ISubmissionsRepo
{
  Task<Submission> CreateAsync(Submission submission);
  Task<Submission?> GetByIdAsync(Guid id);
  Task<IEnumerable<Submission>> GetAllAsync();
  Task DeleteAsync(Guid id);
  // Task UpdateAsync(Submission submission, CancellationToken cancellationToken);
  // Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}