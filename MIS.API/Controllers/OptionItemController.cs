using System.Net;
using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Responses;

namespace MIS.API.Controllers;



[ApiController]
[Route("/api/[controller]")]
public class OptionItemController(IOptionItemRepo optionItemRepo) : ControllerBase
{
  private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;

  [HttpPost]
  public async Task<IActionResult> CreateOptionItems([FromBody] OptionItemsRequestDTO requestDTO)
  {
    var response = await _optionItemRepo.CreateOptionItemAsync(requestDTO);
    return CreatedAtAction(nameof(GetOptionItemsByOptionList), new { optionListId = response.OptionListId },
      ApiResponse<OptionItemsResponseDTO>.SuccessResponse(response, "Option items successfully created", HttpStatusCode.Created)
    );
  }

  [HttpGet]
  [Route("{optionListId:guid}")]
  public async Task<IActionResult> GetOptionItemsByOptionList([FromRoute] Guid optionListId)
  {

    var response = await _optionItemRepo.GetOptionItemsByOptionListIdAsync(optionListId);

    return Ok(ApiResponse<OptionItemsResponseDTO>.SuccessResponse(response, "Option Items fetch successfully"));

  }

  [HttpDelete]
  [Route("{id:guid}")]
  public async Task<IActionResult> DeleteOptionItemById([FromRoute] Guid id)
  {
    await _optionItemRepo.DeleteOptionItemByIdAsync(id);
    return NoContent();
  }

  

  


}