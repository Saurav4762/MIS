using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Common.Models;
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

	[HttpPost]
	[Route("seed")]
	public async Task<IActionResult> SeedMunicipality(IFormFile file)
	{
		var count = await _municipalityService.SeedMunicipalityAsync(new IMunicipalitySeedDTO
		{
			file = new FileDto
			{
				Content = file.OpenReadStream(),
				ContentType = file.ContentType,
				FileName = file.Name,
				SizeInBytes = file.Length
			}
		});

		return Created("", ApiResponse<object>.SuccessResponse(null, $"{count} Records seeded successfully"));
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchMunicipalities([FromQuery] string query, [FromQuery] string? searchBy, [FromQuery] int maxResults = 10, [FromQuery] int pageNumber = 1)
	{
		Console.WriteLine("Search Query: " + query);
		var result = await _municipalityService.SearchMunicipalitiesAsync(query, searchBy, maxResults, pageNumber);
		return Ok(result);
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
