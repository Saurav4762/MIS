using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.Geography;

namespace MIS.Domain.Entities.HouseHold;

public class House : BaseEntity
{
  public Guid ToleId { get; set; }
  public string HouseNumber { get; set; } = null!;
  public string Location { get; set; } = null!;
  public Guid HouseTypeId { get; set; }
  public Guid LandTypeId { get; set; }
  public Guid RoofTypeId { get; set; }
  public Guid WallTypeId { get; set; }
  public string Image { get; set; } = null!;
  public string Purpose { get; set; } = null!;
  public DateTime BuildYear { get; set; }

  // Navigation properties
  public Tole Tole { get; set; } = null!;
  public OptionItem HouseType { get; set; } = null!;
  public OptionItem LandType { get; set; } = null!;
  public OptionItem RoofType { get; set; } = null!;
  public OptionItem WallType { get; set; } = null!;
}
