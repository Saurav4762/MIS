using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Common.Interfaces;
using MIS.Application.Features.Options.OptionItems;
using MIS.Domain.Entities.DataCollection.HouseholdInfo;
using MIS.Domain.Exceptions;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public class LivestockService(
    ILivestockRepo livestockRepo,
    IOptionItemRepo optionItemRepo,
    IValidator<CreateLivestockDto> createValidator,
    IValidator<UpdateLivestockDto> updateValidator,
    IUnitOfWork unitOfWork) : ILivestockService
{
    private readonly ILivestockRepo _livestockRepo = livestockRepo;
    private readonly IOptionItemRepo _optionItemRepo = optionItemRepo;
    private readonly IValidator<CreateLivestockDto> _createValidator = createValidator;
    private readonly IValidator<UpdateLivestockDto> _updateValidator = updateValidator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<LivestockDto> GetLivestockByFamilyIdAsync(Guid familyId)
    {
        var livestock = await _livestockRepo.GetLivestockByFamilyIdAsync(familyId);
        return livestock?.ToLivestockDto()
            ?? throw new NotFoundException(nameof(Family), nameof(Livestock.FamilyId), familyId);
    }

    public async Task<LivestockDto> CreateLivestockAsync(CreateLivestockDto createLivestockDto)
    {
        await _createValidator.EnsureValidOrThrowAsync(createLivestockDto);

        var livestock = new Livestock
        {
            FamilyId = createLivestockDto.FamilyId,
            HasLivestockPractice = createLivestockDto.HasLivestockPractice,
            HasAIServicePractice = createLivestockDto.HasAIServicePractice
        };

        if (createLivestockDto.HasLivestockPractice)
        {
            foreach (var animalDto in createLivestockDto.Animals)
            {
                await ValidateOptionExistsAsync(animalDto.AnimalTypeId);
                livestock.Animals.Add(new LivestockAnimal
                {
                    AnimalTypeId = animalDto.AnimalTypeId,
                    Count = animalDto.Count
                });
            }
        }

        if (createLivestockDto.HasAIServicePractice)
        {
            foreach (var aiServiceDto in createLivestockDto.AIServices)
            {
                await ValidateOptionExistsAsync(aiServiceDto.AnimalTypeId);
                await ValidateOptionExistsAsync(aiServiceDto.BirthHistoryId);
                await ValidateOptionExistsAsync(aiServiceDto.StatusId);

                livestock.AIServices.Add(new LivestockAIService
                {
                    AnimalTypeId = aiServiceDto.AnimalTypeId,
                    AnimalName = aiServiceDto.AnimalName,
                    AgeYears = aiServiceDto.AgeYears,
                    BirthHistoryId = aiServiceDto.BirthHistoryId,
                    SemenOrBullName = aiServiceDto.SemenOrBullName,
                    AiServiceDate = aiServiceDto.AiServiceDate,
                    StatusId = aiServiceDto.StatusId
                });
            }
        }

        var createdLivestock = await _livestockRepo.CreateLivestockAsync(livestock);
        return createdLivestock.ToLivestockDto();
    }

    public async Task<LivestockDto> UpdateLivestockAsync(Guid familyId, UpdateLivestockDto updateLivestockDto)
    {
        await _updateValidator.EnsureValidOrThrowAsync(updateLivestockDto);

        var livestock = await _livestockRepo.GetLivestockByFamilyIdAsync(familyId)
            ?? throw new NotFoundException(nameof(Livestock), nameof(Livestock.FamilyId), familyId);

        if (updateLivestockDto.HasLivestockPractice.HasValue)
            livestock.HasLivestockPractice = updateLivestockDto.HasLivestockPractice.Value;

        if (updateLivestockDto.HasAIServicePractice.HasValue)
            livestock.HasAIServicePractice = updateLivestockDto.HasAIServicePractice.Value;

        // Replace animal list entirely if provided
        if (updateLivestockDto.HasLivestockPractice == true && updateLivestockDto.Animals is not null)
        {
            livestock.Animals.Clear();

            if (livestock.HasLivestockPractice)
            {
                foreach (var animalDto in updateLivestockDto.Animals)
                {
                    await ValidateOptionExistsAsync(animalDto.AnimalTypeId);
                    livestock.Animals.Add(new LivestockAnimal
                    {
                        AnimalTypeId = animalDto.AnimalTypeId,
                        Count = animalDto.Count
                    });
                }
            }
        }

        // Replace AI service list entirely if provided
        if (updateLivestockDto.HasAIServicePractice == true && updateLivestockDto.AIServices is not null)
        {
            livestock.AIServices.Clear();

            if (livestock.HasAIServicePractice)
            {
                foreach (var aiServiceDto in updateLivestockDto.AIServices)
                {
                    await ValidateOptionExistsAsync(aiServiceDto.AnimalTypeId);
                    await ValidateOptionExistsAsync(aiServiceDto.BirthHistoryId);
                    await ValidateOptionExistsAsync(aiServiceDto.StatusId);

                    livestock.AIServices.Add(new LivestockAIService
                    {
                        AnimalTypeId = aiServiceDto.AnimalTypeId,
                        AnimalName = aiServiceDto.AnimalName,
                        AgeYears = aiServiceDto.AgeYears,
                        BirthHistoryId = aiServiceDto.BirthHistoryId,
                        SemenOrBullName = aiServiceDto.SemenOrBullName,
                        AiServiceDate = aiServiceDto.AiServiceDate,
                        StatusId = aiServiceDto.StatusId
                    });
                }
            }
        }
        if(updateLivestockDto.HasLivestockPractice == false)
        {
            livestock.Animals.Clear();
        }
        if(updateLivestockDto.HasAIServicePractice == false)
        {
            livestock.AIServices.Clear();
        }


        await _unitOfWork.SaveChangesAsync();
        return livestock.ToLivestockDto();
    }

    private async Task ValidateOptionExistsAsync(Guid optionId)
    {
        var exists = await _optionItemRepo.CheckIfOptionItemExistsAsync(optionId);
        if (!exists)
        {
            throw new NotFoundException(nameof(OptionItem), nameof(OptionItem.Id), optionId);
        }
    }
}
