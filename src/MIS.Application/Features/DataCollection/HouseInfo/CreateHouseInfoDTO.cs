namespace MIS.Application.Features.DataCollection.HouseInfo;

public class CreateHouseInfoDTO
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
}