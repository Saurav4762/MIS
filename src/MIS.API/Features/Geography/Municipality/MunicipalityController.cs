using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Geography.Municipalities;

namespace MIS.API.Features.Geography.Municipality;

[ApiController]
[Route("/api/[controller]")]
public class MunicipalityController : ControllerBase
{
	private readonly IMunicipalityService _municipalityService;

	public MunicipalityController(IMunicipalityService municipalityService)
	{
		_municipalityService = municipalityService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateMunicipality([FromBody] CreateMunicipalityDTO dto)
	{
		var result = await _municipalityService.CreateMunicipalityAsync(dto);
		return CreatedAtAction(nameof(GetMunicipalityById), new { id = result.Id }, result);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllMunicipalities()
	{
		var result = await _municipalityService.GetAllMunicipalitiesAsync();
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetMunicipalityById(Guid id)
	{
		var result = await _municipalityService.GetMunicipalityByIdAsync(id);
		return Ok(result);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateMunicipality(Guid id, [FromBody] UpdateMunicipalityDTO dto)
	{
		var result = await _municipalityService.UpdateMunicipalityAsync(id, dto);
		return Ok(result);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteMunicipality(Guid id)
	{
		await _municipalityService.DeleteMunicipalityAsync(id);
		return NoContent();
	}
}
