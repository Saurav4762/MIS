using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Post([FromBody] CreateAgricultureDTO dto)
    {
        var created = await _agricultureService.CreateAgricultureAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var agricultures = await _agricultureService.GetAllAgricultureAsync();
        return Ok(agricultures);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var agriculture = await _agricultureService.GetAgricultureByIdAsync(id);
        return Ok(agriculture);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAgricultureDTO dto)
    {
        var updated = await _agricultureService.UpdateAgricultureAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _agricultureService.DeleteAgricultureAsync(id);
        return NoContent();
    }
}
