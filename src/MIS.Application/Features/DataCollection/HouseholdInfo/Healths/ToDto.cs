using MIS.Domain.Entities.DataCollection.HouseholdInfo;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public static class ToDto
{
    public static HealthDto ToHealthDto(this Health health)
    {
        return new HealthDto
        {
            FamilyId = health.FamilyId,
            HasHandwashingPractice = health.HasHandwashingPractice,
            HasNutritionAwareness = health.HasNutritionAwareness,
            HasPregnancyCareAwareness = health.HasPregnancyCareAwareness,
            HasUnder5Checkups = health.HasUnder5Checkups,
            HasIronFolicSupplements = health.HasIronFolicSupplements,
            HasCompleteVaccination = health.HasCompleteVaccination,
            HasHealthInsurance = health.HasHealthInsurance,
            InsuranceProviderId = health.InsuranceProviderId,
            HasChronicIllness = health.HasChronicIllness,
            ChronicIllnessIds = health.ChronicIllnesses.Select(c => c.Id).ToList()
        };
    }
}
