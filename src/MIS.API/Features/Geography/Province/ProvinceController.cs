using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.Geography.Provinces;

namespace MIS.API.Features.Geography.Province;

[ApiController]
[Route("/api/[controller]")]
public class ProvinceController : ControllerBase
{
	private readonly IProvinceService _provinceService;

	public ProvinceController(IProvinceService provinceService)
	{
		_provinceService = provinceService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateProvince([FromBody] CreateProvinceDTO dto)
	{
		var result = await _provinceService.CreateProvinceAsync(dto);
		return CreatedAtAction(
			nameof(GetProvinceById),
			new { id = result.Id },
			ApiResponse<ProvinceDTO>.SuccessResponse(result, "Province created successfully")
		);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllProvinces()
	{
		var result = await _provinceService.GetAllProvincesAsync();
		return Ok(ApiResponse<List<ProvinceDTO>>.SuccessResponse(result));
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchProvinces([FromQuery] string query, [FromQuery] string? searchBy, [FromQuery] int maxResults = 10, [FromQuery] int pageNumber = 1)
	{
		var result = await _provinceService.SearchProvincesAsync(query, searchBy, maxResults, pageNumber);
		return Ok(ApiResponse<List<ProvinceDTO>>.SuccessResponse(result));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetProvinceById(Guid id)
	{
		var result = await _provinceService.GetProvinceByIdAsync(id);
		return Ok(ApiResponse<ProvinceDTO>.SuccessResponse(result));
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateProvince(Guid id, [FromBody] UpdateProvinceDTO dto)
	{
		var result = await _provinceService.UpdateProvinceAsync(id, dto);
		return Ok(ApiResponse<ProvinceDTO>.SuccessResponse(result, "Province updated successfully"));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteProvince(Guid id)
	{
		await _provinceService.DeleteProvinceAsync(id);
		return NoContent();
	}
}
