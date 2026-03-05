using System.Net;
using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Models;
using MIS.API.Interfaces.IRepositories;

using MIS.API.Responses;
using static MIS.API.DTOs.AppRoleDTO;

namespace MIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppRoleController(IAppRoleRepo context) : ControllerBase
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

        return CreatedAtAction(
            actionName: nameof(GetRoleById),
            routeValues: response.Id,
            ApiResponse<object>.SuccessResponse(
                response,
                "Role created sucessfully",
                HttpStatusCode.Created
            ));

    }

    //GET ROLE BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var role = await _repo.GetRoleById(id);



        return Ok(
            ApiResponse<AppRole>.SuccessResponse(
                role,
                "Role deleted sucessfully",
                HttpStatusCode.OK
            ));


    }

    //Revoke Role(Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }



}