using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.API.Data;
using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IServices;
using MIS.API.Responses;

namespace MIS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHashService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthController(
            AppDbContext context,
            IPasswordHashService passwordService,
            ITokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDTOs.LoginRequestDto request)
        {
            var user = await _context.AppUsers
                .Include(x => x.AppUserRoles)
                .ThenInclude(x => x.AppRole)
                .FirstOrDefaultAsync(x => x.Username == request.UserName);

            if (user == null)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "login", new [] { "Invalid username or password" } }
                });
            }
            

            var isValid = _passwordService.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!isValid)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "login", new [] { "Invalid username or password" } }
                });
            }

            var token = await _tokenService.GenerateToken(user);

            var roles = user.AppUserRoles
                .Select(x => x.AppRole.RoleName)
                .ToList();

            var response = new AuthDTOs.LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                FullName = user.FullName,
                Roles = roles
            };

            return Ok(ApiResponse<AuthDTOs.LoginResponseDto>.SuccessResponse(
                response,
                "Login successful"
            ));
        }
    }
}