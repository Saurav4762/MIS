using MIS.Domain.Common.Premitives;
using MIS.Domain.Entities.Geography;

namespace MIS.Domain.Entities.DataCollection.House_info;

public class House : BaseEntity
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