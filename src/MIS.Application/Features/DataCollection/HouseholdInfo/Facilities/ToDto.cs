using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;

public static class ToDto
{
    public static FacilityDto ToFacilityDto(this Facility facility)
    {
        return new FacilityDto
        {
            FamilyId = facility.FamilyId,
            DrinkingWaterId = facility.DrinkingWaterId,
            ToiletTypeId = facility.ToiletTypeId,
            ElectricityId = facility.ElectricityId,
            AltLightId = facility.AltLightId,
            CookingFuelId = facility.CookingFuelId,
            StoveTypeId = facility.StoveTypeId,
            MobilePhone = facility.MobilePhone,
            Radio = facility.Radio,
            Television = facility.Television,
            Computer = facility.Computer,
            Internet = facility.Internet,
            Refrigerator = facility.Refrigerator,
            WashingMachine = facility.WashingMachine
        };
    }
}
