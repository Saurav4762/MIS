using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.Geography.Areas;

namespace MIS.API.Features.Geography.Area;

[ApiController]
[Route("/api/[controller]")]
public class AreaController : ControllerBase
{
	private readonly IAreaService _areaService;

	public AreaController(IAreaService areaService)
	{
		_areaService = areaService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateArea([FromBody] CreateAreaDTO dto)
	{
		var result = await _areaService.CreateAreaAsync(dto);
		return CreatedAtAction(
			nameof(GetAreaById),
			new { id = result.Id },
			ApiResponse<AreaDTO>.SuccessResponse(result, "Area created successfully")
		);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAreas()
	{
		var result = await _areaService.GetAllAreasAsync();
		return Ok(ApiResponse<List<AreaDTO>>.SuccessResponse(result));
	}

	[HttpGet("district/{districtId:guid}")]
	public async Task<IActionResult> GetAreasByDistrictId(Guid districtId)
	{
		var result = await _areaService.GetAreasByDistrictIdAsync(districtId);
		return Ok(ApiResponse<List<AreaDTO>>.SuccessResponse(result));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetAreaById(Guid id)
	{
		var result = await _areaService.GetAreaByIdAsync(id);
		return Ok(ApiResponse<AreaDTO>.SuccessResponse(result));
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateArea(Guid id, [FromBody] UpdateAreaDTO dto)
	{
		var result = await _areaService.UpdateAreaAsync(id, dto);
		return Ok(ApiResponse<AreaDTO>.SuccessResponse(result, "Area updated successfully"));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteArea(Guid id)
	{
		await _areaService.DeleteAreaAsync(id);
		return NoContent();
	}
}

