using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DataCollection.HouseholdInfo;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;

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
    public async Task<IActionResult> Post (CreateFamilyDTO dto)
    {
        await _familyService.CreateFamilyAsync(dto);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById (Guid id)
    {
        
        var result = await _familyService.GetFamilyByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

}