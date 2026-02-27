using Microsoft.AspNetCore.Mvc;
using MIS.API.Dtos;
using MIS.API.Models;
using MIS.API.Repositories;

namespace MIS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReligionController : ControllerBase
{
    private readonly IReligionRepo _religionRepo;
    private readonly ILogger<ReligionController> _logger;

    public ReligionController(IReligionRepo religionRepo, ILogger<ReligionController> logger)
    {
        _religionRepo = religionRepo;
        _logger = logger;
    }

    // GET: api/religion
    [HttpGet]
    public async Task<IActionResult> GetReligions()
    {
        try
        {
            _logger.LogInformation("Getting all religions");
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
            _logger.LogInformation("Getting religion by id {Id}", id);
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
    public async Task<IActionResult> PostReligion(ReligionRequestDto dto )
    {
        try
        {
            _logger.LogInformation("Creating new religion");
            var newReligion = await _religionRepo.AddReligionAsync(dto);
            return CreatedAtAction(nameof(GetReligions), //Name of the action to retrive the resource
                new { id = newReligion.Id }, //Route values
                newReligion); //The object to return in the response delay.
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid data provided for creating religion");
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            //Internal server error for unexpected reasons 
            _logger.LogError(e, "Error occured while creating new religion");
            return StatusCode(500, new { message = "An error occured while creating religion." });

        }
    }

    // PUT: api/religion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReligion(Guid id, ReligionRequestDto dto)
    {
        try
        {
            _logger.LogInformation("Updating religion {Id}", id);
            var updatedReligion = await _religionRepo.UpdateReligionAsync(id, dto);
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
            _logger.LogInformation("Deleting religion {Id}", id);
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