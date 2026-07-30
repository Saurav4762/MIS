namespace MIS.Application.Features.DataCollection.HouseholdInfo.Healths;

public class CreateHealthDto
{
    public Guid FamilyId { get; set; }

    // Section 01: Awareness
    public bool HasHandwashingPractice { get; set; }
    public bool HasNutritionAwareness { get; set; }
    public bool HasPregnancyCareAwareness { get; set; }
    public bool HasUnder5Checkups { get; set; }
    public bool HasIronFolicSupplements { get; set; }
    public bool HasCompleteVaccination { get; set; }

    // Section 02: Health Insurance
    public bool HasHealthInsurance { get; set; }
    public Guid? InsuranceProviderId { get; set; } // required only when HasHealthInsurance = true

    // Section 03: Chronic Illness
    public bool HasChronicIllness { get; set; }
    public List<Guid> ChronicIllnessIds { get; set; } = []; // required only when HasChronicIllness = true
}
