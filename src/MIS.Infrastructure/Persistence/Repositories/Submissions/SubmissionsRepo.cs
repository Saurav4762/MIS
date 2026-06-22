using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Submissions;
using MIS.Domain.Entities.Submissions;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Submissions;


public class SubmissionsRepo : ISubmissionsRepo
{
  private readonly ApplicationDbContext _context;

  public SubmissionsRepo(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Submission> CreateAsync(Submission submission)
  {
    submission.Id = Guid.NewGuid();
    submission.SubmittedAt = DateTime.UtcNow;
    _context.Submissions.Add(submission);
    await _context.SaveChangesAsync();
    return submission;
  }

  public async Task<Submission?> GetByIdAsync(Guid id)
  {
    return await _context.Submissions.FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<IEnumerable<Submission>> GetAllAsync()
  {
    return await _context.Submissions.ToListAsync();
  }

  public Task DeleteAsync(Guid id)
  {
    var submission = new Submission { Id = id };
    _context.Submissions.Attach(submission);
    _context.Submissions.Remove(submission);
    return _context.SaveChangesAsync();

  }
}