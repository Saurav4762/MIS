using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Interfaces.IServices;
using MIS.API.Models;
using MIS.API.Responses;
using static MIS.API.DTOs.PersonDTO;

namespace MIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PersonController : ControllerBase
{
    private readonly IPersonService _service;

    public PersonController(IPersonService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        
        return Ok(
            ApiResponse<IEnumerable<PersonResponseDto>>.SuccessResponse(
                result
                )
            );
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
         var result = await _service.GetByIdAsync(id);

         return Ok(
             ApiResponse<PersonDTO.PersonResponseDto>.SuccessResponse(
                 result
                 )
             );
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(
            ApiResponse<PersonResponseDto>.SuccessResponse(
                result,
                "Person Created Successfully"
            )
        );
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreatePersonDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(
            ApiResponse<PersonResponseDto>.SuccessResponse(
                null,
                "Person updated successfully"
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(
            ApiResponse<PersonResponseDto>.SuccessResponse(
                null,
                "Person deleted successfully"
            )
        );
    }
    
}