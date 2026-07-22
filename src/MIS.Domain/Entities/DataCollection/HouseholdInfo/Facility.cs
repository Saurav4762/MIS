using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Facility: BaseEntity
{
    //Foreign 
    public Guid FamilyId { get; set; }
    public Guid? DrinkingWaterId { get; set; }
    public Guid? ToiletTypeId { get; set; }
    public Guid? ElectricityId { get; set; }
    public Guid? AltLightId { get; set; }
    public Guid? CookingFuelId { get; set; }
    public Guid? StoveTypeId { get; set; }
    
     //Info
     public bool MobilePhone { get; set; } 
     public bool Radio { get; set; }
     public bool Television { get; set;  }
     public bool Computer { get; set; }
     public bool Internet { get; set; }
     public bool Refrigerator { get; set;  }
     public bool WashingMachine { get; set; }
     
    // Navigation 
    public Family Family { get; set; } = null!;
    public OptionItem? DrinkingWater { get; set; }
    public OptionItem? ToiletType { get; set; }
    public OptionItem? Electricity { get; set; }
    public OptionItem? AltLight { get; set; }
    public OptionItem? CookingFuel { get; set; }
    public OptionItem? StoveType { get; set; }
}