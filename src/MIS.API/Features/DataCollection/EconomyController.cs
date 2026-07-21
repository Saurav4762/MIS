using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Economies;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class EconomyController(IEconomyService economyService) : ControllerBase
{
  private readonly IEconomyService _economyService = economyService;
  // GET: api/Economy
  [HttpGet("{familyId}")]
  public async Task<IActionResult> GetEconomyData([FromRoute] Guid familyId)
  {
    var economyData = await _economyService.GetEconomyByFamilyIdAsync(familyId);
    // Logic to retrieve economy data goes here
    return Ok(ApiResponse<EconomyDto>.SuccessResponse(economyData));
  }

  [HttpPost]
  public async Task<IActionResult> CreateEconomyData([FromBody] CreateEconomyDto dto)
  {
    var economyData = await _economyService.CreateEconomyAsync(dto);
    return CreatedAtAction(nameof(GetEconomyData), new { familyId = dto.FamilyId }, ApiResponse<EconomyDto>.SuccessResponse(economyData, "Economy data created successfully"));
  }

}