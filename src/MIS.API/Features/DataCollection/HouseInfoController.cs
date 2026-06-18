using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DataCollection.HouseInfo;

namespace MIS.API.Features.DataCollection;


[ApiController]
[Route("api/[controller]")]
public class HouseInfoController : ControllerBase
{

  public HouseInfoController(IHouseInfoService houseInfoService)
  {
  }


  [HttpPost]
  public IActionResult Post()
  {
    return CreatedAtAction(nameof(Get), new { }, null);
  }


  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }
}