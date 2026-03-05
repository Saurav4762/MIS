using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Interfaces.IRepositories;

namespace MIS.API.Controllers;
[ApiController]
[Route ("api/[controller]")]
public class MunicipalityController : ControllerBase
{
    private readonly IMunicipalityRepo _municipalityRepo;

    public MunicipalityController(IMunicipalityRepo municipalityRepo)
    {
        _municipalityRepo = municipalityRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMunicipality([FromBody] MunicipalityRequest request)
    {
        var municipality = await _municipalityRepo.CreateMunicipality(request.NameEn, request.NameNe, request.Code);
        var response = new MunicipalityResponse
        {
            NameEn = municipality.NameEn,
            NameNe = municipality.NameNe,
            Code = municipality.Code,
        };
        return CreatedAtAction(nameof(GetById), new{ id = municipality.Id}, response);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var municipality = await _municipalityRepo.GetById(id);
        return Ok(municipality);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Updated(Guid id,[FromBody] MunicipalityRequest request)
    {
        var municipality = await _municipalityRepo.UpdateMunicipality(id, request.NameEn, request.NameNe, request.Code);
        var response = new MunicipalityResponse
        {
            NameEn = municipality.NameEn,
            NameNe = municipality.NameNe,
            Code = municipality.Code,
        };
        return Ok(response);
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _municipalityRepo.DeleteMunicipality(id);
        return NoContent();
    }
}