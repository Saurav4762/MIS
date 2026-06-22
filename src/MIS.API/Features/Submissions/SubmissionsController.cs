using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.Submissions;
using MIS.Domain.Exceptions;

namespace MIS.API.Features.Submissions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubmissionsController(ISubmissionsService submissionService) : ControllerBase
{
  private readonly ISubmissionsService _submissionService = submissionService;


  [HttpPost]
  public async Task<IActionResult> Create()
  {

    var sub = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;


    if (!Guid.TryParse(sub, out var userId))
    {
      // Handle missing/invalid user ID
      throw new UnauthorizedException("Invalid token: User ID is missing");
    }

    var submission = await _submissionService.CreateAsync(new CreateSubmissionDTO { SubmittedById = userId });

    return CreatedAtAction(nameof(GetById), new { id = submission.Id }, ApiResponse<SubmissionDTO>.SuccessResponse(submission, "Submission created successfully", System.Net.HttpStatusCode.Created));
  }


  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    var submission = await _submissionService.GetByIdAsync(id);
    return Ok(ApiResponse<SubmissionDTO>.SuccessResponse(submission));
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var submissions = await _submissionService.GetAllAsync();
    return Ok(ApiResponse<IEnumerable<SubmissionDTO>>.SuccessResponse(submissions));
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await _submissionService.DeleteAsync(id);
    return Ok(ApiResponse<bool>.SuccessResponse(true, "Submission deleted successfully"));
  }
}