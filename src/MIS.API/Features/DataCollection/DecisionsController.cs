using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;

namespace MIS.API.Features.DataCollection;


[Route("api/[controller]")]
public class DecisionsController : ControllerBase
{
  private readonly IDecisionService _decisionService;

  public DecisionsController(IDecisionService decisionService)
  {
    _decisionService = decisionService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateDecision([FromBody] CreateDecisionDTO createDecisionDTO)
  {
    var result = await _decisionService.CreateDecision(createDecisionDTO);
    return Ok(result);
  }

  [HttpGet("{familyId}")]
  public async Task<IActionResult> GetDecisionByFamilyId(Guid familyId)
  {
    var result = await _decisionService.GetDecisionByFamilyId(familyId);
    return Ok(result);
  }
}