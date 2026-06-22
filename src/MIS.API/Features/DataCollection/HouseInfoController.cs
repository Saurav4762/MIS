using Microsoft.AspNetCore.Mvc;
using MIS.Application.Features.DataCollection.HouseInfo;

namespace MIS.API.Features.DataCollection;


[ApiController]
[Route("api/[controller]")]
public class HouseInfoController : ControllerBase
{

  private readonly IHouseInfoService _houseInfoService;

  public HouseInfoController(IHouseInfoService houseInfoService)
  {
    _houseInfoService = houseInfoService;
  }


  [HttpPost]
  public IActionResult Post(CreateHouseInfoDTO createHouseInfoDTO)
  {
    _houseInfoService.CreateHouseAsync(createHouseInfoDTO);
    return CreatedAtAction(nameof(Get), new { }, null);
  }


  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }
}