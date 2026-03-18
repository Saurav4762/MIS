using FluentValidation;
using MIS.API.Interfaces.IRepositories;
using MIS.API.DTOs;
using MIS.API.Interfaces.IServices;
using MIS.API.Validators;
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
    public async Task<WardDTO.WardResponse> CreateAsync(WardDTO.WardRequest dto)
    {
        //VLIDATE using validator and throw validation exception if not valid
        var validator = new CreateWardValidator(_wardRepo);
        var validationResult = await validator.ValidateAsync(dto);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }
        
        //BUSINESS LOGIC - no checks here, validation already done in the validator 
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
            MunicipalityCode = wardWithMunicipality?.Municipality?.Code ?? string.Empty,
            MunicipalityName = wardWithMunicipality?.Municipality?.NameEn ?? string.Empty
          
        };
    }

    

    public async Task<PaginatedResponse<WardResponse>> GetAllAsync(PaginationRequest request)
    {
       var pagedinWards = await _wardRepo.GetAllWardsAsync(request.PageNumber, request.PageSize);
       
       if(pagedinWards == null || !pagedinWards.Data.Any())
           return new PaginatedResponse<WardResponse>
           {
               Data = new List<WardResponse>(),
               TotalCount = 0,
               PageNumber = request.PageNumber,
               PageSize = request.PageSize
           };

       var wardResponses = pagedinWards.Data.Select(w => new WardResponse
       {
           Id = w.Id,
           WardName = w.Name,
           WardCode = w.Code,
           MunicipalityId = w.MunicipalityId,
           MunicipalityCode = w.Municipality?.Code ?? string.Empty,
           MunicipalityName = w.Municipality?.NameEn ?? string.Empty
       }).ToList();

       return new PaginatedResponse<WardResponse>
       {
           Data = wardResponses,
           TotalCount = pagedinWards.TotalCount,
           PageNumber = pagedinWards.PageNumber,
           PageSize = pagedinWards.PageSize
       };
    }

    public async Task<WardResponse> GetByIdAsync(Guid id)
    {
        //VALIDATE using validator
        var validator = new WardIdValidator(_wardRepo);
        var validationResult = await validator.ValidateAsync(id);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }

        //BUSINESS LOGIC - no checks here, validation already done in the validator
        var ward = await  _wardRepo.GetWardByIdAsync(id);
        
        if (ward == null)
            throw new NotFoundException("ward not found", "Id", id);
            
        return new WardResponse
        {
            Id = ward.Id,
            WardName = ward.Name,
            WardCode = ward.Code,
            MunicipalityId = ward.MunicipalityId,
            MunicipalityCode = ward.Municipality?.Code ?? string.Empty,
            MunicipalityName = ward.Municipality?.NameEn ?? string.Empty
        };
    }

    public async Task<WardResponse> UpdateAsync(Guid id, WardRequest dto)
    {
        //VLIDATE ward exists and request is valid using validator and throw validation exception if not valid
        var validator = new WardIdValidator(_wardRepo);
        var validationResult = await validator.ValidateAsync(id);

         if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }
        
        //VALIDATE update ward
        var updateValidator = new UpdateWardValidator(_wardRepo, id);
        var updateValidationResult = await updateValidator.ValidateAsync(dto);

        if (!updateValidationResult.IsValid)
        {
            var errors = updateValidationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }

        //BUSINESS LOGIC - no checks here, validation already done in the validator
        var ward = await  _wardRepo.GetWardByIdAsync(id);
        
        
        ward.Name = dto.Name;
        ward.Code = dto.Code;
        ward.MunicipalityId = dto.MunicipalityId;
        
        
        await _wardRepo.UpdateAsync(ward);
        
        var updatedWard = await _wardRepo.GetWardWithMunicipality(id);
        
        if (updatedWard == null)
            throw new NotFoundException("ward not found", "Id", id);

        return new WardResponse
        {
            Id = updatedWard.Id,
            MunicipalityId = updatedWard.MunicipalityId,
            MunicipalityCode = updatedWard.Municipality?.Code ?? string.Empty,
            MunicipalityName = updatedWard.Municipality?.NameEn ?? string.Empty,
            WardName = updatedWard.Name,
            WardCode = updatedWard.Code
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        //VALIDATE using validator
        var validator = new DeleteWardValidator(_wardRepo);
        var validationResult = await validator.ValidateAsync(id);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            throw new ValidationException(errors);
        }
        //BUSINESS LOGIC - no checks here, validation already done in the validator
        var ward = await  _wardRepo.GetWardByIdAsync(id);

        if (ward == null)
            throw new NotFoundException("ward not found", "Id", id);

        await _wardRepo.DeleteWardById(ward);
    }
}