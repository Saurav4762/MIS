namespace MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

public class UpdateLivestockDto
{
    public bool? HasLivestockPractice { get; set; }
    public bool? HasAIServicePractice { get; set; }
    public List<CreateLivestockAnimalDto>? Animals { get; set; }
    public List<CreateLivestockAIServiceDto>? AIServices { get; set; }
}
