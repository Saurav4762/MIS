namespace MIS.Application.Features.DataCollection.HouseholdInfo.Socials;

public class SocialDto
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid EthnicityId { get; set; }
    public Guid ReligionId { get; set; }
    public Guid MotherTongueId { get; set; }
    public Guid CommonLanguageId { get; set; }
}