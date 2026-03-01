using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;
using MIS.API.Responses;

namespace MIS.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AppRoleController(IAppRoleRepo context) :ControllerBase
{
    private readonly IAppRoleRepo _repo = context;
    
    //Create role
    [HttpPost]
    public async Task<IActionResult> CreateRole(AppRoleDTO.CreateRoleDto dto)
    {
        var role = new AppRole
        {
            Id = Guid.NewGuid(),

            RoleName = dto.RoleName,
            RoleCode = dto.RoleCode
        };

        var created = await _repo.CreateAsync(role);

        var response = new AppRoleDTO.RoleResponseDto
        {
            Id = created.Id,
            RoleName = created.RoleName,
            RoleCode = created.RoleCode
        };

        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Role created sucessfully",
                200
                ));

    }
    
    //GET ROLE BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var role = await _repo.GetRoleById(id);
        if (role == null)
        {
            return NotFound(
                ApiResponse<object>.FailResponse(
                    "Role not found",
                    "ROLE_NOT_FOUND",
                    $"Role with id {id} does not exist",
                    400

                )
            );
        }

        

        var response = new AppRoleDTO.RoleResponseDto
        {
            Id = role.Id,
            RoleName = role.RoleName,
            RoleCode = role.RoleCode

        };
        
        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Role deleted sucessfully",
                200
            ));
        
        
    }
    
    //Revoke Role(Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(
                ApiResponse<object>.FailResponse(
                    "Role not found",
                   "ROLE_NOT_FOUND",
                    $"Role with id {id} does not exist",
                     400
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Role found successfully",
                200
                ));
    }


    
}