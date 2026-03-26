using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.Options.OptionItems;

namespace MIS.API.Features.Options;

[ApiController]
[Route("/api/[controller]")]
public class OptionItemController : ControllerBase
{
	private readonly IOptionItemService _optionItemService;

	public OptionItemController(IOptionItemService optionItemService)
	{
		_optionItemService = optionItemService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateOptionItem([FromBody] CreateOptionItemDTO dto)
	{
		var result = await _optionItemService.CreateOptionItem(dto);
		return Ok(result);
	}

	[HttpGet("OptionList/{optionListId:guid}")]
	public async Task<IActionResult> GetOptionItemsByOptionListId(Guid optionListId)
	{
		var result = await _optionItemService.GetOptionItemsByOptionListId(optionListId);
		return Ok(result);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> UpdateOptionItem(Guid id, [FromBody] UpdateOptionItemDTO dto)
	{
		var result = await _optionItemService.UpdateOptionItem(id, dto);
		return Ok(result);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteOptionItem(Guid id)
	{
		await _optionItemService.DeleteOptionItem(id);
		return NoContent();
	}
}
