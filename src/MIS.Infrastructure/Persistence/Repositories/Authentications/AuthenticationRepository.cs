using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Authentication;
using MIS.Domain.Entities.Identity;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Authentications;

public class AuthenticationRepository : IAuthenticationRepository
{
  private readonly ApplicationDbContext _context;

  public AuthenticationRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
  }
}