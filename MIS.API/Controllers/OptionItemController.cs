using System.Net;
using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Models;
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
  [Route("{id:guid}")]
  public async Task<IActionResult> GetOptionItemById([FromRoute] Guid id)
  {
    var response = await _optionItemRepo.GetOptionItemByIdAsync(id);
    return Ok(ApiResponse<OptionItemResponseDTO>.SuccessResponse(response, $"{nameof(OptionItem)} fetched successfully"));
  }



  [HttpGet]
  [Route("optionList/{optionListId:guid}")]
  public async Task<IActionResult> GetOptionItemsByOptionList([FromRoute] Guid optionListId)
  {

    var response = await _optionItemRepo.GetOptionItemsByOptionListIdAsync(optionListId);

    return Ok(ApiResponse<OptionItemsResponseDTO>.SuccessResponse(response, "Option Items fetch successfully"));

  }

  [HttpPatch]
  [Route("{id:guid}")]
  public async Task<IActionResult> UpdateOptionItemById([FromRoute] Guid id, UpdateOptionItemRequestDTO requestDTO)
  {
    var response = await _optionItemRepo.UpdateOptionItemAsync(id, requestDTO);
    return Ok(ApiResponse<OptionItemResponseDTO>.SuccessResponse(response, $"{nameof(OptionItem)} updated successfully", HttpStatusCode.OK));
  }

  [HttpDelete]
  [Route("{id:guid}")]
  public async Task<IActionResult> DeleteOptionItemById([FromRoute] Guid id)
  {
    await _optionItemRepo.DeleteOptionItemByIdAsync(id);
    return NoContent();
  }






}