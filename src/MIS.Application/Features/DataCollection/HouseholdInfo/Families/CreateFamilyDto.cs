
namespace MIS.Application.Features.DataCollection.HouseholdInfo.Families;

public class CreateFamilyDto
{
    public Guid SubmissionId { get; set; }
    public Guid ResidenceHouseId { get; set; }
    public Guid? ResidentTypeId { get; set; }
    
    
    
    // Can extend with sub-forms here
}