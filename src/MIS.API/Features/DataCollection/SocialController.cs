using Microsoft.AspNetCore.Mvc;
using MIS.API.Common.Responses;
using MIS.Application.Features.DataCollection.HouseholdInfo.Socials;


namespace MIS.API.Features.DataCollection; 

[ApiController]
[Route("api/[controller]")]
public class SocialController : ControllerBase
{
    private readonly ISocialService _socialService;

    public SocialController(ISocialService socialService)
    {
        _socialService = socialService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSocialDto dto)
    {
        var id = await _socialService.CreateSocialAsync(dto);
        var created = await _socialService.GetSocialByIdAsync(id);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<SocialDto>.SuccessResponse(created, "Social record created successfully", System.Net.HttpStatusCode.Created)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _socialService.GetSocialByIdAsync(id);

        if (result == null)
            return NotFound(ApiResponse<SocialDto>.FailResponse("NOT_FOUND", "Social record not found", statusCode: System.Net.HttpStatusCode.NotFound));

        return Ok(ApiResponse<SocialDto>.SuccessResponse(result));
    }
}