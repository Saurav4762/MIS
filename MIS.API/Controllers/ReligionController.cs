using System.Net;
using Microsoft.AspNetCore.Mvc;
using MIS.API.Dtos;
using MIS.API.Models;
using MIS.API.Repositories;
using MIS.API.Repositories.Interfaces;
using MIS.API.Responses;

namespace MIS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReligionController(IReligionRepo religionRepo) : ControllerBase
{
    private readonly IReligionRepo _religionRepo = religionRepo;



    // GET: api/religion
    [HttpGet]
    public async Task<IActionResult> GetAllReligion()
    {
        var religions = await _religionRepo.GetReligionsAsync();
        return Ok(ApiResponse<List<Religion>>.SuccessResponse(religions));
    }

    // GET: api/religion/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReligionById(Guid id)
    {

        var religion = await _religionRepo.GetReligionByIdAsync(id);
        return Ok(ApiResponse<Religion>.SuccessResponse(religion));
    }

    // POST: api/religion
    [HttpPost]
    public async Task<IActionResult> CreateReligion(ReligionRequest dto)
    {

        var religion = await _religionRepo.AddReligionAsync(dto.NameEn, dto.NameNe);

        return CreatedAtAction(
            actionName: nameof(GetReligionById),
            routeValues: new { id = religion.Id },
            value: ApiResponse<Religion>.SuccessResponse(religion, "Religion created succesfully", statusCode: HttpStatusCode.Created));
    }

    // PUT: api/religion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReligion(Guid id, ReligionRequest dto)
    {
        try
        {
            var updatedReligion = await _religionRepo.UpdateReligionAsync(id, dto.NameEn, dto.NameNe);
            ReligionResponse response = new ReligionResponse
            {
                NameEn = updatedReligion.NameEn,
                NameNe = updatedReligion.NameNe
            };
            return Ok(new { message = "Religion updated successfully", data = updatedReligion });
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE: api/religion/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReligion(Guid id)
    {
        try
        {

            await _religionRepo.DeleteReligionAsync(id);
            return Ok(new { message = "Religion deleted successfully" });

        }

        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Religion not found" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }

    }
}