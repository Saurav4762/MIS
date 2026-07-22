using System.Runtime.InteropServices.Marshalling;
using FluentValidation;
using Microsoft.VisualBasic.CompilerServices;
using MIS.Application.Common.Extensions;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.Agricultures;

public class AgricultureService : IAgricultureService
{
    private readonly IAgricultureRepo _repo;
    private readonly IValidator<CreateAgricultureDTO> _createValidation;
    private readonly IValidator<UpdateAgricultureDTO> _updateValidator;

    public AgricultureService(
        IAgricultureRepo repo,
        IValidator<CreateAgricultureDTO> createValidator,
        IValidator<UpdateAgricultureDTO> updateValidator)
    {
        _repo = repo;
        _createValidation = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<AgricultureDTO> CreateAgricultureAsync(CreateAgricultureDTO dto)
    {
        await _createValidation.EnsureValidOrThrowAsync(dto);
        var agriculture = new Agriculture
        {
            Id = Guid.NewGuid(),
            FamilyId = dto.FamilyId,
            LandUnitId = dto.LandUnitId,
            OwnershipStatusId = dto.OwnershipStatusId,
            TotalArea = dto.TotalArea,
            UsesImprovedSeeds = dto.UsesImprovedSeeds,
            UsesChemicalPesticides = dto.UsesChemicalPesticides,

            SelectedCrops = dto.SelectedCrops.Select(crops => new AgricultureCrop
            {
                Id = Guid.NewGuid(),
                CropId = crops.CropId,
                AreaInHectares = crops.AreaInHectares,
                EstimatedYield = crops.EstimatedYield,
                Notes = crops.Notes
            }).ToList(),

            Equipments = dto.Equipments.Select(equipment => new AgricultureEquipment
            {
                Id = Guid.NewGuid(),
                EquipmentId = equipment.EquipmentId,
                Quantity = equipment.Quantity ?? 0
            }).ToList(),

            LandTypes = dto.LandTypes.Select(landType => new AgricultureLandType
            {
                Id = Guid.NewGuid(),
                LandTypeId = landType.LandTypeId,
                Area = landType.Area,
            }).ToList(),

            ProblemsFaced = dto.ProblemsFaced.Select(problem => new AgricultureProblem
            {
                Id = Guid.NewGuid(),
                ProblemId = problem.ProblemId,
                Details = problem.Details
            }).ToList()
        };

        var created = await _repo.CreateAgricultureAsync(agriculture);
        return ToDto(created);
    }


    //Update code 
   public async Task<AgricultureDTO> UpdateAgricultureAsync(Guid id, UpdateAgricultureDTO dto)
{
    await _updateValidator.ValidateAndThrowAsync(dto);

    var agriculture = await _repo.GetAgricultureByIdAsync(id);

    if (agriculture == null)
    {
        throw new NotFoundException(nameof(Agriculture), nameof(Agriculture.Id), id);
    }

    if (dto.LandUnitId.HasValue)
    {
        agriculture.LandUnitId = dto.LandUnitId.Value;
    }

    if (dto.OwnershipStatusId.HasValue)
    {
        agriculture.OwnershipStatusId = dto.OwnershipStatusId.Value;
    }

    if (dto.TotalArea.HasValue)
    {
        agriculture.TotalArea = dto.TotalArea.Value;
    }

    if (dto.UsesImprovedSeeds.HasValue)
    {
        agriculture.UsesImprovedSeeds = dto.UsesImprovedSeeds.Value;
    }

    if (dto.UsesChemicalPesticides.HasValue)
    {
        agriculture.UsesChemicalPesticides = dto.UsesChemicalPesticides.Value;
    }

    var updated = await _repo.UpdateAgricultureAsync(agriculture);
    return ToDto(updated);
}


    //Get by id 
    public async Task<AgricultureDTO> GetAgricultureByIdAsync(Guid id)
    {
        var agriculture = await _repo.GetAgricultureByIdAsync(id);
        if (agriculture == null)
        {
            throw new NotFoundException(nameof(Agriculture), nameof(Agriculture.Id), id);
        }
        return ToDto(agriculture);
    }

    public async Task<List<AgricultureDTO>> GetAllAgricultureAsync()
    {
        var agricultures = await _repo.GetAllAgriculturesAsync();
        var result = new List<AgricultureDTO>();
        foreach (var agriculture in agricultures)
        {
            result.Add(ToDto(agriculture));
        }
        return result;
    }

    public async Task DeleteAgricultureAsync(Guid id)
    {
        await _repo.DeleteAgricultureAsync(id);
    }


    // This is the place where the DB Entity change to DTOs
    private AgricultureDTO ToDto(Agriculture agriculture)
    {
        return new AgricultureDTO
        {
            Id = agriculture.Id,
            FamilyId = agriculture.FamilyId,
            LandUnitId = agriculture.LandUnitId,
            OwnershipStatusId = agriculture.OwnershipStatusId,
            TotalArea = agriculture.TotalArea,
            UsesImprovedSeeds = agriculture.UsesImprovedSeeds,
            UsesChemicalPesticides = agriculture.UsesChemicalPesticides,

            SelectedCrops = agriculture.SelectedCrops.Select(crop => new AgricultureCropDTO
            {
                CropId = crop.CropId,
                AreaInHectares = crop.AreaInHectares,
                EstimatedYield = crop.EstimatedYield,
                Notes = crop.Notes
            }).ToList(),

            Equipments = agriculture.Equipments.Select(equipment => new AgricultureEquipmentDTO
            {
                EquipmentId = equipment.EquipmentId,
                Quantity = equipment.Quantity
            }).ToList(),

            LandTypes = agriculture.LandTypes.Select(landType => new AgricultureLandTypeDTO
            {
                LandTypeId = landType.LandTypeId,
                Area = landType.Area
            }).ToList(),

            ProblemsFaced = agriculture.ProblemsFaced.Select(problem => new AgricultureProblemDTO
            {
                ProblemId = problem.ProblemId,
                Details = problem.Details
            }).ToList()
        };
    }
}