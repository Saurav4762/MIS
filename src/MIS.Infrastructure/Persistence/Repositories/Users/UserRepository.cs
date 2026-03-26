using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.Authentication;
using MIS.Application.Features.Users;
using MIS.Domain.Entities.Identity;
using MIS.Infrastructure.Persistence.Data;

namespace MIS.Infrastructure.Persistence.Repositories.Users;

public class UserRepository : IUserRepository
{
  readonly ApplicationDbContext _context;

  public UserRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<User> CreateUserAsync(User user)
  {
    await _context.AddAsync(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<bool> ExistingUserByEmailAsync(string email)
  {
    return await _context.Users.AnyAsync(x => x.Email == email);
  }

  public async Task<bool> ExistingUserByUsernameAsync(string username)
  {
    return await _context.Users.AnyAsync(x => x.Username == username);
  }

}