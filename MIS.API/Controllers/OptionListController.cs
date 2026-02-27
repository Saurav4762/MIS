using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OptionListController(IOptionList optionListRepository) : ControllerBase
{
  private readonly IOptionList _optionListRepository = optionListRepository;

  [HttpPost]
  public async Task<IActionResult> CreateOptionList([FromBody] OptionListRequest request)
  {
    try
    {
      var optionList = await _optionListRepository.CreateOptionListAsync(request.Code, request.LabelEn, request.LabelNe, request.Description);
      var response = new OptionListResponse
      {
        Code = optionList.Code,
        LabelEn = optionList.LabelEn,
        LabelNe = optionList.LabelNe,
        Description = optionList.Description
      };
      return CreatedAtAction(nameof(GetOptionListById), new { id = optionList.Id }, response);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetOptionListById(Guid id)
  {
    try
    {
      var optionList = await _optionListRepository.GetOptionListByIdAsync(id);
      OptionListResponse response = new OptionListResponse
      {
        Code = optionList.Code,
        LabelEn = optionList.LabelEn,
        LabelNe = optionList.LabelNe,
        Description = optionList.Description
      };
      return Ok(response);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateOptionList(Guid id, [FromBody] OptionListRequest request)
  {
    try
    {
      var updatedOptionList = await _optionListRepository.UpdateOptionListAsync(id, request.LabelEn, request.LabelNe, request.Description);
      OptionListResponse response = new OptionListResponse
      {
        Code = updatedOptionList.Code,
        LabelEn = updatedOptionList.LabelEn,
        LabelNe = updatedOptionList.LabelNe,
        Description = updatedOptionList.Description
      };
      return Ok(response);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteOptionList(Guid id)
  {
    try
    {
      await _optionListRepository.DeleteOptionListAsync(id);
      return NoContent();
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}