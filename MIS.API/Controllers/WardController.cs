using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Responses;
using System.Net;
using MIS.API.Interfaces.IServices;
using static MIS.API.DTOs.WardDTO;

[ApiController]
[Route("api/[controller]")]
public class WardController : ControllerBase
{
    private readonly IWardService  _wardService;
    
    private readonly IMunicipalityRepo _municipalityRepo;

    public WardController(IMunicipalityRepo municipalityRepo, IWardService wardService)
    {
        
        _municipalityRepo = municipalityRepo;
        _wardService = wardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _wardService.GetAllAsync();

        return Ok(ApiResponse<IEnumerable<WardResponse>>.SuccessResponse(result));


    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWardById(Guid id)
    {
      var ward = await _wardService.GetByIdAsync(id);

      return Ok(ApiResponse<WardResponse>.SuccessResponse(ward));
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(WardDTO.WardRequest dto)
    {
        var result = await _wardService.CreateAsync(dto);
        
        return Ok(ApiResponse<WardResponse>.SuccessResponse(
            result,
            "Ward created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWard(Guid id,WardDTO.WardRequest dto)
    {
      await _wardService.UpdateAsync(id, dto);
      
      return Ok(
          ApiResponse<WardResponse>.SuccessResponse(
              null,
              "Ward updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        
        await _wardService.DeleteAsync(id);
        
        return Ok(
            ApiResponse<string>.SuccessResponse(
                null,
                "Ward deleted successfully"));
    }
    
}