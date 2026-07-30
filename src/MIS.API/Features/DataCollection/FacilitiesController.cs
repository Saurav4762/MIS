using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class FacilitiesController(IFacilityService facilityService) : ControllerBase
{
    private readonly IFacilityService _facilityService = facilityService;

    [HttpGet("{familyId}")]
    public async Task<IActionResult> GetFacilityData([FromRoute] Guid familyId)
    {
        var facilityData = await _facilityService.GetFacilityByFamilyIdAsync(familyId);
        return Ok(ApiResponse<FacilityDto>.SuccessResponse(facilityData));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFacilityData([FromBody] CreateFacilityDto dto)
    {
        var facilityData = await _facilityService.CreateFacilityAsync(dto);
        return CreatedAtAction(nameof(GetFacilityData), new { familyId = dto.FamilyId }, ApiResponse<FacilityDto>.SuccessResponse(facilityData, "Facility data created successfully"));
    }

    [HttpPut("{familyId}")]
    public async Task<IActionResult> UpdateFacilityData([FromRoute] Guid familyId, [FromBody] UpdateFacilityDto dto)
    {
        var facilityData = await _facilityService.UpdateFacilityAsync(familyId, dto);
        return Ok(ApiResponse<FacilityDto>.SuccessResponse(facilityData, "Facility data updated successfully"));
    }
}
