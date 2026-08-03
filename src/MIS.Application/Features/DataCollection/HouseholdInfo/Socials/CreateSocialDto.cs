namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public class CreateSocialDto
{
    public Guid FamilyId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid ReligionId { get; set; }
    public Guid MotherTongueId { get; set; }
    public Guid CommonLanguageId { get; set; }
}