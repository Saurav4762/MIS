using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.HouseHold;

public class Family : BaseEntity
{
  public Guid HouseId { get; set; }
  public Guid HeadOfTheFamilyId { get; set; }

  public Guid EthnicityId { get; set; }
  public Guid ReligionId { get; set; }

  //Navigation Property
  public OptionItem? Ethnicity { get; set; } = null;

  public OptionItem? Religion { get; set; } = null;

  public Person? HeadOfTheFamily { get; set; } = null;

}
