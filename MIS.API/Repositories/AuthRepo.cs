using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IServices;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Repositories;

public class AuthRepo : IAuthRepo
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHashService _passwordHashService;

    public AuthRepo(AppDbContext dbContext, IPasswordHashService passwordHashService)
    {
        _dbContext = dbContext;
        _passwordHashService = passwordHashService;
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
        user.PasswordHash = _passwordHashService.Hash(password);

        _dbContext.AppUsers.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<AppUser> LoginAsync(string username, string password)
    {
        var user = await _dbContext.AppUsers
            .FirstOrDefaultAsync(x => x.Username == username && x.IsActive) ?? throw new UnauthorizedException("Invalid username or password");

        var result = _passwordHashService.Verify(password, user.PasswordHash);

        if (!result)
            throw new UnauthorizedException("Invalid username or password");

        return user;
    }
}