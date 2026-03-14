using MIS.API.Interfaces.IRepositories;
using MIS.API.DTOs;
using MIS.API.Interfaces.IServices;
using static MIS.API.DTOs.WardDTO;
namespace MIS.API.Services;
using MIS.API.Models;
using MIS.API.Exceptions;

public class WardService : IWardService
{
    
    private readonly IWardRepo _wardRepo;

    public WardService(IWardRepo wardRepo)
    {
        _wardRepo = wardRepo;
    }

    private async Task ValidateCreateAsync(WardRequest dto)
    {
        var errors = new Dictionary<string, string[]>();
        
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors["Name"] = ["Ward Name is required"];
        
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors["Code"] = ["Ward Code is required"];
        
        if (dto.MunicipalityId == Guid.Empty)
            errors["MunicipalityId"] = ["Municipality Id is required"];
        
        if(!await _wardRepo.MunicipalityExistsAsync(dto.MunicipalityId))
            errors["Municipality Id"] = ["Municipality does not exist"];
        
        if(await _wardRepo.WardExistsAsync(dto.Name, dto.MunicipalityId))
            errors["Name"] = ["Ward Name already exists"];
        
        if(errors.Any())
            throw new ValidationException(errors);
    }

    private async Task ValidateUpdateAsync(WardRequest dto, Guid id)
    {
        var errors = new Dictionary<string, string[]>();
        
        if(string.IsNullOrWhiteSpace(dto.Name))
            errors["Name"] = ["Ward Name is required"];
        
        if(await _wardRepo.WardNameExistsAsync(dto.Name,dto.MunicipalityId, id))
            errors["Name"] =  ["Ward Name is already exists in this municipality"];
        
        if(errors.Any())
            throw new ValidationException(errors);
    }
    public async Task<WardDTO.WardResponse> CreateAsync(WardDTO.WardRequest dto)
    {
        
        await ValidateCreateAsync(dto);
        
        var ward = new Ward
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Code = dto.Code,
            MunicipalityId = dto.MunicipalityId,
        };
        
        var createWard = await _wardRepo.CreateWardAsync(ward );
        
        var wardWithMunicipality = await _wardRepo.GetWardWithMunicipality(createWard.Id);

        return new WardResponse
        {
            Id = createWard.Id,
            WardName = createWard.Name,
            WardCode = createWard.Code,
            MunicipalityId = createWard.MunicipalityId,
            MunicipalityCode = wardWithMunicipality.Municipality.Code,
            MunicipalityName = wardWithMunicipality.Municipality.NameEn
          
        };
    }

    

    public async Task<IEnumerable<WardResponse>> GetAllAsync()
    {
       var wards = await _wardRepo.GetAllWardsAsync();
       
       if(wards == null)
           return new List<WardResponse>();

       return wards.Select(w => new WardResponse
       {
           Id = w.Id,
           WardName = w.Name,
           WardCode = w.Code,
           MunicipalityId = w.MunicipalityId,
           MunicipalityCode = w.Municipality?.Code,
           MunicipalityName = w.Municipality?.NameEn
       });
    }

    public async Task<WardDTO.WardResponse> GetByIdAsync(Guid id)
    {
        var ward = await  _wardRepo.GetWardByIdAsync(id);
        if(ward == null)
            throw new NotFoundException("ward not found","Id",id);

        return new WardResponse
        {
            Id = ward.Id,
            WardName = ward.Name,
            WardCode = ward.Code,
            MunicipalityId = ward.MunicipalityId,
            MunicipalityCode = ward.Municipality?.Code,
            MunicipalityName = ward.Municipality?.NameEn
        };
    }

    public async Task<WardResponse> UpdateAsync(Guid id, WardRequest dto)
    {
        await ValidateUpdateAsync(dto, id);
        
        var ward = await  _wardRepo.GetWardByIdAsync(id);
        
        if(ward == null)
            throw new NotFoundException("ward not found", "Id",id);
        
        ward.Name = dto.Name;
        ward.Code = dto.Code;
        ward.MunicipalityId = dto.MunicipalityId;
        
        
        await _wardRepo.UpdateAsync(ward);
        
        var updatedWard = await _wardRepo.GetWardWithMunicipality(id);

        return new WardResponse
        {
            Id = updatedWard.Id,
            MunicipalityId = updatedWard.MunicipalityId,
            MunicipalityCode = updatedWard.Municipality.Code,
            MunicipalityName = updatedWard.Municipality.NameEn,
            WardName = updatedWard.Name,
            WardCode = updatedWard.Code
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        
        var ward = await  _wardRepo.GetWardByIdAsync(id);
        if(ward == null)
            throw new NotFoundException("ward not found", "Id",id);
        
        await _wardRepo.DeleteWardById(ward);
    }
}