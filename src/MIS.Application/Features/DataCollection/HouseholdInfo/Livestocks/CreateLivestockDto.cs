namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public class CreateLivestockDto
{
    public Guid FamilyId { get; set; }
    public bool HasLivestockPractice { get; set; }
    public bool HasAIServicePractice { get; set; }
    public List<CreateLivestockAnimalDto> Animals { get; set; } = [];
    public List<CreateLivestockAIServiceDto> AIServices { get; set; } = [];
}

public class CreateLivestockAnimalDto
{
    public Guid AnimalTypeId { get; set; }
    public int Count { get; set; }
}

public class CreateLivestockAIServiceDto
{
    public Guid AnimalTypeId { get; set; }
    public string AnimalName { get; set; } = null!;
    public int AgeYears { get; set; }
    public Guid BirthHistoryId { get; set; }
    public string SemenOrBullName { get; set; } = null!;
    public string AiServiceDate { get; set; } = null!;
    public Guid StatusId { get; set; }
}
