using Microsoft.AspNetCore.Mvc;
using MIS.API.DTOs;
using MIS.API.Exceptions;
using MIS.API.Models;
using MIS.API.Interfaces.IRepositories;
using MIS.API.Responses;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class WardController : ControllerBase
{
    private readonly IWardRepo _repo;
    
    private readonly IMunicipalityRepo _municipalityRepo;

    public WardController(IWardRepo repo, IMunicipalityRepo municipalityRepo)
    {
        _repo = repo;
        _municipalityRepo = municipalityRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var wards = await _repo.GetAllWardsAsync();

        return Ok(ApiResponse<List<Ward>>.SuccessResponse(wards));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWardById(Guid id)
    {
        // 1️⃣ Get ward by id
        var ward = await _repo.GetWardByIdAsync(id);
        if (ward == null)
            throw new NotFoundException("Ward", id, id);

        // 2️⃣ Get municipality to include its name
        var municipality = await _municipalityRepo.GetById(ward.MunicipalityId);
        if (municipality == null)
            return NotFound(ApiResponse<WardDTO.WardResponse>.FailResponse("Municipality not found"));

        // 3️⃣ Map to response DTO
        var response = new WardDTO.WardResponse
        {
            Id = ward.Id,
            WardName = ward.Name,
            WardCode = ward.Code,
            MunicipalityId = ward.MunicipalityId,
            MunicipalityName = municipality.NameEn, // ✅ add municipality name
            MunicipalityCode = municipality.Code
        };

        // 4️⃣ Return response
        return Ok(ApiResponse<WardDTO.WardResponse>.SuccessResponse(response));
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(WardDTO.WardRequest dto)
    {
        // 1️⃣ Get municipality
        var municipality = await _municipalityRepo.GetById(dto.MunicipalityId);

        if (municipality == null)
            throw new NotFoundException($"Municipality not found", dto.MunicipalityId, dto.MunicipalityId);

        // 2️⃣ Create ENTITY (NOT DTO)
        var ward = new Ward
        {
            Id = Guid.NewGuid(),
            MunicipalityId = municipality.Id,
            Name = dto.Name,
            Code = dto.Code
        };

        // 3️⃣ Save entity (FIXED: passing ward instead of WaitCallback)
        await _repo.CreateWardAsync(ward.Name, ward.Code,ward.MunicipalityId);

        // 4️⃣ Convert ENTITY → RESPONSE DTO (FIXED: using municipality.Code)
        var response = new WardDTO.WardResponse
        {
            Id = ward.Id,
            MunicipalityId = ward.MunicipalityId,
            MunicipalityCode = municipality.Code,
            WardName = ward.Name,
            WardCode = ward.Code
        };

        // 5️⃣ Return response
        return CreatedAtAction(
            nameof(GetWardById),
            new { id = ward.Id },
            ApiResponse<WardDTO.WardResponse>.SuccessResponse(
                response,
                "Ward Created Successfully"
            )
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWard(Guid id,WardDTO.WardRequest dto)
    {
        try
        {
            var updateWard = await _repo.UpdateAsync(id, dto.Name, dto.Code);
            WardDTO.WardResponse response = new()
            {
                WardName = updateWard.Name,
                WardCode = updateWard.Code,
            };

            return Ok(new { message = "Ward updated successfully", data = updateWard });

        }

        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
            
        }
        
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
            
        }
     
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _repo.DeleteWardById(id);
            return Ok(new { messsage = "Ward deleted successfully" });

        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }

        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }

    }
    
}