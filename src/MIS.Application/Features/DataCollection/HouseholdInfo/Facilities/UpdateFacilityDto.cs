namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public class UpdateFacilityDto
{
    public Guid? DrinkingWaterId { get; set; }
    public Guid? ToiletTypeId { get; set; }
    public Guid? ElectricityId { get; set; }
    public Guid? AltLightId { get; set; }
    public Guid? CookingFuelId { get; set; }
    public Guid? StoveTypeId { get; set; }

    public bool? MobilePhone { get; set; }
    public bool? Radio { get; set; }
    public bool? Television { get; set; }
    public bool? Computer { get; set; }
    public bool? Internet { get; set; }
    public bool? Refrigerator { get; set; }
    public bool? WashingMachine { get; set; }
}