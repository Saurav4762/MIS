using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Families;


namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _familyService;

    public FamilyController(IFamilyService familyService)
    {
        _familyService = familyService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post (CreateFamilyDto dto)
    {
        await _familyService.CreateFamilyAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.SubmissionId }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById (Guid id)
    {
        
        var familydata = await _familyService.GetFamilyByIdAsync(id);
        if (familydata == null) return NotFound();
        return Ok(ApiResponse<FamilyDto>.SuccessResponse(familydata));
    }

}