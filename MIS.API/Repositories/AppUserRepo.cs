using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class AppUserRepo : IAppUserRepo
{
    private readonly AppDbContext _dbContext;
    private readonly PasswordHasher<AppUser> _passwordHasher;

    public AppUserRepo(AppDbContext dbContext, PasswordHasher<AppUser>  passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        return await _dbContext.AppUsers
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<AppUser> RegisterUserAsync(AppUser user, string password)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        
        _dbContext.AppUsers.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<AppUser?> LoginAsync(string username, string password)
    {
        var user = await _dbContext.AppUsers
            .FirstOrDefaultAsync(x=>x.Username == username && x.IsActive);
        
        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password");
        
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash!,
            password);
        
        return result == PasswordVerificationResult.Success ? user : null;
        
    }
}