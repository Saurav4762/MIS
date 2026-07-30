using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DataCollection.HouseholdInfo;

namespace MIS.API.Features.DataCollection;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberDTO dto)
    {
        var result = await _memberService.CreateMemberAsync(dto);
        return CreatedAtAction(nameof(GetMemberById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMemberById(Guid id)
    {
        var result = await _memberService.GetMemberByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("family/{familyId:guid}")]
    public async Task<IActionResult> GetMembersByFamilyId(Guid familyId)
    {
        var result = await _memberService.GetMembersByFamilyIdAsync(familyId);
        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateMember(Guid id, [FromBody] UpdateMemberDTO dto)
    {
        var result = await _memberService.UpdateMemberAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        await _memberService.DeleteMemberAsync(id);
        return NoContent();
    }
}
