using Microsoft.AspNetCore.Mvc;

namespace MIS.API.Features.DataCollection;
[ApiController]
[Route("api/[controller]")]
public class LivestockController : ControllerBase
{
  [HttpPost]
  public IActionResult Create()
  {
    return Ok();
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }

  [HttpPatch]
  public IActionResult Update()
  {
    return Ok();
  }
}