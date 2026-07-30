using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public static class ToDto
{
    public static LivestockDto ToLivestockDto(this Livestock livestock)
    {
        return new LivestockDto
        {
            FamilyId = livestock.FamilyId,
            HasLivestockPractice = livestock.HasLivestockPractice,
            HasAIServicePractice = livestock.HasAIServicePractice,
            Animals = livestock.Animals.Select(a => a.ToLivestockAnimalDto()).ToList(),
            AIServices = livestock.AIServices.Select(s => s.ToLivestockAIServiceDto()).ToList()
        };
    }

    public static LivestockAnimalDto ToLivestockAnimalDto(this LivestockAnimal animal)
    {
        return new LivestockAnimalDto
        {
            Id = animal.Id,
            LivestockId = animal.LivestockId,
            AnimalTypeId = animal.AnimalTypeId,
            Count = animal.Count
        };
    }

    public static LivestockAIServiceDto ToLivestockAIServiceDto(this LivestockAIService aiService)
    {
        return new LivestockAIServiceDto
        {
            Id = aiService.Id,
            LivestockId = aiService.LivestockId,
            AnimalTypeId = aiService.AnimalTypeId,
            AnimalName = aiService.AnimalName,
            AgeYears = aiService.AgeYears,
            BirthHistoryId = aiService.BirthHistoryId,
            SemenOrBullName = aiService.SemenOrBullName,
            AiServiceDate = aiService.AiServiceDate,
            StatusId = aiService.StatusId
        };
    }
}
