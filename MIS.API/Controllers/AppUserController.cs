using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MIS.API.Dtos;
using MIS.API.Repositories.Interfaces;
using MIS.API.Services;
using MIS.API.Exceptions;
using MIS.API.Models;

namespace MIS.API.Controllers;
[ApiController]
[Route("api/auth")]
public class AppUserController : ControllerBase
{
    private readonly IAppUserRepo _appUserRepo;
    private readonly JwtService _jwtService;

    public AppUserController(IAppUserRepo appUserRepo, JwtService jwtService)
    {
        _appUserRepo = appUserRepo;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AppUserDTO.RegisterDto dto)
    {
        var existing = await _appUserRepo.GetByUsernameAsync(dto.userName);
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

        await _appUserRepo.RegisterUserAsync(user, dto.Password);

        return Ok("User Registered sucessfully");
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(AppUserDTO.LoginDto dto)
    {
        var user = await _appUserRepo.LoginAsync(dto.UserName,dto.Password);

        if (user == null)
            throw new UnauthorizedException("Invalid Username or password");
        
        var token = _jwtService.GenerateToken(user);
        return Ok(token);
        
    }
}