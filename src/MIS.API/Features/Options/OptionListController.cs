using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Options.OptionLists;

namespace MIS.API.Features.Options;


[ApiController]
[Route("/api/[controller]")]
public class OptionListController : ControllerBase
{
  private readonly IOptionListService _optionListService;

  public OptionListController(
    IOptionListService optionListService
    )
  {
    _optionListService = optionListService;

  }

  [HttpPost]
  public async Task<IActionResult> CreateOptionList([FromBody] CreateOptionListDTO dto)
  {
    var result = await _optionListService.CreateOptionListAsync(dto);
    return CreatedAtAction(nameof(GetOptionListById), new { id = result.Id }, result);
  }

  [HttpGet]
  public async Task<IActionResult> GetAllOptionLists()
  {
    var result = await _optionListService.GetAllOptionListsAsync();
    return Ok(result);
  }

  [HttpGet]
  [Route("{id:guid}")]
  public async Task<IActionResult> GetOptionListById(Guid id)
  {
    // IOptionListService currently has no GetById method, so resolve from current list.
    var optionList = (await _optionListService.GetAllOptionListsAsync())
      .FirstOrDefault(x => x.Id == id);

    if (optionList is null)
      return NotFound();

    return Ok(optionList);
  }

  [HttpPatch("{id:guid}")]
  public async Task<IActionResult> UpdateOptionList(Guid id, [FromBody] UpdateOptionListDTO dto)
  {
    var result = await _optionListService.UpdateOptionListAsync(id, dto);
    return Ok(result);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteOptionList(Guid id)
  {
    await _optionListService.DeleteOptionListByIdAsync(id);
    return NoContent();
  }
}