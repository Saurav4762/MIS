using Microsoft.AspNetCore.Mvc;
using MIS.API.Dtos;
using MIS.API.Models;
using MIS.API.Repositories;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReligionController(IReligionRepo religionRepo) : ControllerBase
{
    private readonly IReligionRepo _religionRepo = religionRepo;



    // GET: api/religion
    [HttpGet]
    public async Task<IActionResult> GetReligions()
    {
        try
        {
            var religions = await _religionRepo.GetReligionsAsync();
            return Ok(new { message = "Religions retrieved successfully", data = religions });

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    // GET: api/religion/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReligionById(Guid id)
    {
        try
        {
           
            var religion = await _religionRepo.GetReligionByIdAsync(id);

            if (religion == null)
                return NotFound(new { message = "Religion not found" });

            return Ok(new { message = "Religion retrieved successfully", data = religion });
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }

    // POST: api/religion
    [HttpPost]
    public async Task<IActionResult> CreateReligion(ReligionRequest dto )
    {
        try
        {
           
            var religion = await _religionRepo.AddReligionAsync(dto.NameEn, dto.NameNe);
           var respone = new ReligionResponse
           {
             NameEn = religion.NameEn,
             NameNe = religion.NameNe  
           };

           return CreatedAtAction(nameof(GetReligionById),new { id = religion.Id});
        }
        catch (ArgumentException ex)
        {
           
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            //Internal server error for unexpected reasons 
           
            return StatusCode(500, new { message = "An error occured while creating religion." });

        }
    }

    // PUT: api/religion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReligion(Guid id, ReligionRequest dto)
    {
        try
        {
           
            var updatedReligion = await _religionRepo.UpdateReligionAsync(id, dto.NameEn,dto.NameNe);
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
           return BadRequest(new {message = e.Message});
        }
        
    }
}