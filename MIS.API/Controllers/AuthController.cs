using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MIS.API.Dtos;
using MIS.API.Repositories.Interfaces;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Responses;
using MIS.API.Interfaces.IServices;

namespace MIS.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepo _authRepo;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthRepo authRepo, ITokenService tokenService)
    {
        _authRepo = authRepo;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AppUserDTO.RegisterDto dto)
    {
        var existing = await _authRepo.GetByUsernameAsync(dto.userName);
        if (existing != null)
        {
            throw new BadRequestException("Username already exists");
        }

        var user = new AppUser
        {
            Username = dto.userName,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone
        };

        await _authRepo.RegisterUserAsync(user, dto.Password);

        return Ok("User Registered sucessfully");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(AppUserDTO.LoginDto dto)
    {
        var user = await _authRepo.LoginAsync(dto.UserName, dto.Password);

        if (user == null)
            throw new UnauthorizedException("Invalid Username or password");

        var token = _tokenService.GenerateToken(user).Result;
        return Ok(ApiResponse<object>.SuccessResponse(new { Token = token }, "Logged in successfully"));

    }
}