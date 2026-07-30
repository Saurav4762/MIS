using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class HealthController(IHealthService healthService) : ControllerBase
{
    private readonly IHealthService _healthService = healthService;

    [HttpGet("{familyId}")]
    public async Task<IActionResult> GetHealthData([FromRoute] Guid familyId)
    {
        var healthData = await _healthService.GetHealthByFamilyIdAsync(familyId);
        return Ok(ApiResponse<HealthDto>.SuccessResponse(healthData));
    }

    [HttpPost]
    public async Task<IActionResult> CreateHealthData([FromBody] CreateHealthDto dto)
    {
        var healthData = await _healthService.CreateHealthAsync(dto);
        return CreatedAtAction(nameof(GetHealthData), new { familyId = dto.FamilyId }, ApiResponse<HealthDto>.SuccessResponse(healthData, "Health data created successfully"));
    }

    [HttpPatch("{familyId}")]
    public async Task<IActionResult> UpdateHealthData([FromRoute] Guid familyId, [FromBody] UpdateHealthDto dto)
    {
        var healthData = await _healthService.UpdateHealthAsync(familyId, dto);
        return Ok(ApiResponse<HealthDto>.SuccessResponse(healthData, "Health data updated successfully"));
    }
}
