using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Models;
using MIS.API.Interfaces.IRepositories;

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
    public async Task<IActionResult> CreateEthnicity([FromBody] EthnicityRequest request)
    {
        try
        {
            var ethnicity = await _ethnicityRepo.CreateEthnicity(request.NameEn, request.NameNe);
            var response = new EthnicityResponse
            {
                NameNe = ethnicity.NameNe,
                NameEn = ethnicity.NameEn,
            };
            return CreatedAtAction(nameof(GetById), new { id = ethnicity.Id }, response);
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
                var response = new EthnicityResponse
                {
                    NameNe = ethnicity.NameNe,
                    NameEn = ethnicity.NameEn,
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEthnicity(Guid id,  EthnicityRequest request)
    {
        try
        {
            var ethnicity = await _ethnicityRepo.UpdateEthnicity(id, request.NameEn, request.NameNe);
            var response = new EthnicityResponse
            {
                NameNe = ethnicity.NameNe,
                NameEn = ethnicity.NameEn,
            };
            return Ok(response);
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
           
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}