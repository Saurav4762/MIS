namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public class LivestockDto
{
    public Guid FamilyId { get; set; }
    public bool HasLivestockPractice { get; set; }
    public bool HasAIServicePractice { get; set; }
    public List<LivestockAnimalDto> Animals { get; set; } = [];
    public List<LivestockAIServiceDto> AIServices { get; set; } = [];
}

public class LivestockAnimalDto
{
    public Guid Id { get; set; }
    public Guid LivestockId { get; set; }
    public Guid AnimalTypeId { get; set; }
    public int Count { get; set; }
}

public class LivestockAIServiceDto
{
    public Guid Id { get; set; }
    public Guid LivestockId { get; set; }
    public Guid AnimalTypeId { get; set; }
    public string AnimalName { get; set; } = null!;
    public int AgeYears { get; set; }
    public Guid BirthHistoryId { get; set; }
    public string SemenOrBullName { get; set; } = null!;
    public string AiServiceDate { get; set; } = null!;
    public Guid StatusId { get; set; }
}
