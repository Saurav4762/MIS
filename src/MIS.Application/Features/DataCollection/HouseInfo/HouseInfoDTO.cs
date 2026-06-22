using MIS.Domain.Entities.Geography;

namespace MIS.Application.Features.DataCollection.HouseInfo;

public class HouseInfoDTO
{
  public Guid SubmissionId { get; set; }

  public Guid WardId { get; set; }
  public Guid ToleId { get; set; }

  // House Identification
  public string HouseNumber { get; set; } = null!;
  public string Location { get; set; } = null!;

  public Guid ImageId { get; set; }

  //House Characteristics 
  public Guid HouseTypeId { get; set; }
  public Guid LandTypeId { get; set; }
  public Guid RoofTypeId { get; set; }
  public Guid WallTypeId { get; set; }


  // --- Navigation Properties ---
  public Tole Tole { get; set; } = null!;

  public Ward Ward { get; set; } = null!;
  public OptionItem HouseType { get; set; } = null!;
  public OptionItem LandType { get; set; } = null!;
  public OptionItem RoofType { get; set; } = null!;
  public OptionItem WallType { get; set; } = null!;

}