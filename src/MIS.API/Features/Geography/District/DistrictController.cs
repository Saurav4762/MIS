using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.Geography.Districts;

namespace MIS.API.Features.Geography.District;

[ApiController]
[Route("/api/[controller]")]
public class DistrictController : ControllerBase
{
	private readonly IDistrictService _districtService;

	public DistrictController(IDistrictService districtService)
	{
		_districtService = districtService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictDTO dto)
	{
		var result = await _districtService.CreateDistrictAsync(dto);
		return CreatedAtAction(
			nameof(GetDistrictById),
			new { id = result.Id },
			ApiResponse<DistrictDTO>.SuccessResponse(result, "District created successfully")
		);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllDistricts()
	{
		var result = await _districtService.GetAllDistrictsAsync();
		return Ok(ApiResponse<List<DistrictDTO>>.SuccessResponse(result));
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchDistricts([FromQuery] string query, [FromQuery] string? searchBy, [FromQuery] int maxResults = 10, [FromQuery] int pageNumber = 1)
	{
		var result = await _districtService.SearchDistrictsAsync(query, searchBy, maxResults, pageNumber);
		return Ok(ApiResponse<List<DistrictDTO>>.SuccessResponse(result));
	}

	[HttpGet("province/{provinceId:guid}")]
	public async Task<IActionResult> GetDistrictsByProvinceId(Guid provinceId)
	{
		var result = await _districtService.GetDistrictsByProvinceIdAsync(provinceId);
		return Ok(ApiResponse<List<DistrictDTO>>.SuccessResponse(result));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetDistrictById(Guid id)
	{
		var result = await _districtService.GetDistrictByIdAsync(id);
		return Ok(ApiResponse<DistrictDTO>.SuccessResponse(result));
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateDistrict(Guid id, [FromBody] UpdateDistrictDTO dto)
	{
		var result = await _districtService.UpdateDistrictAsync(id, dto);
		return Ok(ApiResponse<DistrictDTO>.SuccessResponse(result, "District updated successfully"));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteDistrict(Guid id)
	{
		await _districtService.DeleteDistrictAsync(id);
    return NoContent();
	}
}
