using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class LivestockController(ILivestockService livestockService) : ControllerBase
{
    private readonly ILivestockService _livestockService = livestockService;

    [HttpGet("{familyId}")]
    public async Task<IActionResult> GetLivestockData([FromRoute] Guid familyId)
    {
        var livestockData = await _livestockService.GetLivestockByFamilyIdAsync(familyId);
        return Ok(ApiResponse<LivestockDto>.SuccessResponse(livestockData));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLivestockData([FromBody] CreateLivestockDto dto)
    {
        var livestockData = await _livestockService.CreateLivestockAsync(dto);
        return CreatedAtAction(nameof(GetLivestockData), new { familyId = dto.FamilyId }, ApiResponse<LivestockDto>.SuccessResponse(livestockData, "Livestock data created successfully"));
    }

    [HttpPatch("{familyId}")]
    public async Task<IActionResult> UpdateLivestockData([FromRoute] Guid familyId, [FromBody] UpdateLivestockDto dto)
    {
        var livestockData = await _livestockService.UpdateLivestockAsync(familyId, dto);
        return Ok(ApiResponse<LivestockDto>.SuccessResponse(livestockData, "Livestock data updated successfully"));
    }
}
