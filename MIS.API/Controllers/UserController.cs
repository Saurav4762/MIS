using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;
using BCrypt.Net;
using MIS.API.Responses;

namespace MIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepo _repo;
    
    private readonly IAppRoleRepo _roleRepo;

    public UserController(IUserRepo repo, IAppRoleRepo roleRepo)
    {
        _repo = repo;
        _roleRepo = roleRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repo.GetAllUsers();

        var response = users.Select(u => new UserDTOs.UserResponse
        {
            Id = u.Id,
            Username = u.Username,
            FullName = u.FullName,
            Email = u.Email,
            Phone = u.Phone,
            IsActive = u.IsActive,
            Created = u.CreatedAt

        });
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _repo.GetUsersByIdAsync(id);

        var response = new UserDTOs.UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Phone = user.Phone,
            Email = user.Email,
            IsActive = user.IsActive,
            Created = user.CreatedAt
        };
        
        return Ok(response);

    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDTOs.CreateUserRequest request)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow

        };
        
        var created = await _repo.CreateUserAsync(user);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(AppRoleDTO.AssignRoleDto dto)
    {
        var user = await _repo.GetUsersByIdAsync(dto.UserId);
        
        var role = await _roleRepo.GetRoleByIdAsync(dto.RoleId);

        return Ok(user);
    }

    [HttpPut("{id}")]
     public async Task<IActionResult> Update(Guid id, UserDTOs.UpdateUserRequest request)
     {
         var user = await _repo.GetUsersByIdAsync(id);

         if (user == null)
         {
             return NotFound(ApiResponse<string>.FailResponse(
                 "User not found"
                     ));
         }
         
         user.FullName = request.FullName ?? user.FullName;
         user.Phone = request.Phone ?? user.Phone;
         user.Email = request.Email ?? user.Email;
         user.IsActive = request.IsActive;

         try
         {

             var updated = await _repo.UpdateUserAsync(id,user);

             var response = new UserDTOs.UserResponse
             {
                 Id = updated.Id,
                 Username = updated.Username,
                 FullName = updated.FullName,
                 Phone = updated.Phone,
                 Email = updated.Email,
                 IsActive = updated.IsActive,
                 Created = updated.CreatedAt
             };
             
             return Ok(ApiResponse<UserDTOs.UserResponse>.SuccessResponse(
                 response,
                 "User Updated Successfully"));

         }
         catch (Exception e)
         {
             return BadRequest(ApiResponse<string>.FailResponse(
                 ""));

         }



     }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _repo.DeleteUserAsync(id);
        
        if (!result) return NotFound();

        return Ok("user Deleted");
    }
    
}