using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.Agricultures;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class AgricultureController : ControllerBase
{
    private readonly IAgricultureService _agricultureService;

    public AgricultureController(IAgricultureService agricultureService)
    {
        _agricultureService = agricultureService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAgriculture([FromBody] CreateAgricultureDTO dto)
    {
        var created = await _agricultureService.CreateAgricultureAsync(dto);
        return CreatedAtAction(nameof(GetAgricultureById), new { id = created.Id },
            ApiResponse<AgricultureDTO>.SuccessResponse(created, "Agriculture created successfully", System.Net.HttpStatusCode.Created));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAgricultures()
    {
        var agricultures = await _agricultureService.GetAllAgricultureAsync();
        return Ok(ApiResponse<List<AgricultureDTO>>.SuccessResponse(agricultures));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAgricultureById(Guid id)
    {
        var agriculture = await _agricultureService.GetAgricultureByIdAsync(id);
        return Ok(ApiResponse<AgricultureDTO>.SuccessResponse(agriculture));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAgriculture(Guid id, [FromBody] UpdateAgricultureDTO dto)
    {
        var updated = await _agricultureService.UpdateAgricultureAsync(id, dto);
        return Ok(ApiResponse<AgricultureDTO>.SuccessResponse(updated, "Agriculture updated successfully"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAgriculture(Guid id)
    {
        await _agricultureService.DeleteAgricultureAsync(id);
        return Ok(ApiResponse<bool>.SuccessResponse(true, "Agriculture deleted successfully"));
    }
}
