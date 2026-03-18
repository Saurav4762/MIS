using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Geography.Toles;

namespace MIS.API.Features.Geography.Tole;

[ApiController]
[Route("/api/[controller]")]
public class ToleController : ControllerBase
{
	private readonly IToleService _toleService;

	public ToleController(IToleService toleService)
	{
		_toleService = toleService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateTole([FromBody] CreateToleDTO dto)
	{
		var result = await _toleService.CreateToleAsync(dto);
		return CreatedAtAction(nameof(GetToleById), new { id = result.Id }, result);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllToles()
	{
		var result = await _toleService.GetAllTolesAsync();
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetToleById(Guid id)
	{
		var result = await _toleService.GetToleByIdAsync(id);
		return Ok(result);
	}

	[HttpGet("ward/{wardId:guid}")]
	public async Task<IActionResult> GetTolesByWardId(Guid wardId)
	{
		var result = await _toleService.GetTolesByWardIdAsync(wardId);
		return Ok(result);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateTole(Guid id, [FromBody] UpdateToleDTO dto)
	{
		var result = await _toleService.UpdateToleAsync(id, dto);
		return Ok(result);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteTole(Guid id)
	{
		await _toleService.DeleteToleAsync(id);
		return NoContent();
	}
}
