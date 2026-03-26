using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Geography.Wards;

namespace MIS.API.Features.Geography.Ward;

[ApiController]
[Route("/api/[controller]")]
public class WardController : ControllerBase
{
	private readonly IWardService _wardService;

	public WardController(IWardService wardService)
	{
		_wardService = wardService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateWard([FromBody] CreateWardDTO dto)
	{
		var result = await _wardService.CreateWardAsync(dto);
		return CreatedAtAction(nameof(GetWardById), new { id = result.Id }, result);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllWards()
	{
		var result = await _wardService.GetAllWardsAsync();
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetWardById(Guid id)
	{
		var result = await _wardService.GetWardByIdAsync(id);
		return Ok(result);
	}

	[HttpGet("municipality/{municipalityId:guid}")]
	public async Task<IActionResult> GetWardsByMunicipalityId(Guid municipalityId)
	{
		var result = await _wardService.GetWardsByMunicipalityIdAsync(municipalityId);
		return Ok(result);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateWard(Guid id, [FromBody] UpdateWardDTO dto)
	{
		var result = await _wardService.UpdateWardAsync(id, dto);
		return Ok(result);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteWard(Guid id)
	{
		await _wardService.DeleteWardAsync(id);
		return NoContent();
	}
}
