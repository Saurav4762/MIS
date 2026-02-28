using Microsoft.AspNetCore.Mvc;
using MIS.API.Models;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EthnicityController : ControllerBase
{
    private readonly IEthnicityRepo _ethnicityRepo;

    public EthnicityController(IEthnicityRepo ethnicityRepo)
    {
        _ethnicityRepo = ethnicityRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEthnicity(string nameEn , string nameNe)
    {
        try
        {
            var ethnicity = await _ethnicityRepo.CreateEthnicity(nameEn, nameNe);
            return CreatedAtAction(nameof(GetById), new { id = ethnicity.Id }, ethnicity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var ethnicity = await _ethnicityRepo.GetById(id);
                return Ok(ethnicity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEthnicity(Guid id,string nameEn, string nameNe)
    {
        try
        {
            var ethnicity = await _ethnicityRepo.UpdateEthnicity(id, nameEn, nameNe);
            return Ok(new {message ="updated sucessfully",data = ethnicity});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEthnicity(Guid id)
    {
        try
        {
            var ethnicity = await _ethnicityRepo.DeleteEthnicity(id);
            if (ethnicity == null)
            {
                return NotFound();                    // 404 - standard REST response
            }
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}