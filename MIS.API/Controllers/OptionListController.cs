using Microsoft.AspNetCore.Mvc;
using MIS.API.Repositories.Interfaces;

namespace MIS.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OptionListController(IOptionList optionListRepository) : ControllerBase
{
  private readonly IOptionList _optionListRepository = optionListRepository;

  [HttpPost]
  public async Task<IActionResult> CreateOptionList(string code, string nameEn, string nameNe, string description)
  {
    try
    {
      var optionList = await _optionListRepository.CreateOptionListAsync(code, nameEn, nameNe, description);
      return CreatedAtAction(nameof(GetOptionListById), new { id = optionList.Id }, optionList);
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
      return Ok(optionList);
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
  public async Task<IActionResult> UpdateOptionList(Guid id, string nameEn, string nameNe, string description)
  {
    try
    {
      var updatedOptionList = await _optionListRepository.UpdateOptionListAsync(id, nameEn, nameNe, description);
      return Ok(updatedOptionList);
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