using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Health : BaseEntity
{
    // Foreign Keys
    public Guid FamilyId { get; set; }
    public Guid? InsuranceProviderId { get; set; } // Nullable: Only active if HasHealthInsurance is true

    // Section 01: Awareness
    public bool HasHandwashingPractice { get; set; }
    public bool HasNutritionAwareness { get; set; }
    public bool HasPregnancyCareAwareness { get; set; }
    public bool HasUnder5Checkups { get; set; }
    public bool HasIronFolicSupplements { get; set; }
    public bool HasCompleteVaccination { get; set; }

    // Section 02: Health Insurance
    public bool HasHealthInsurance { get; set; }

    // Section 03: Chronic Illness
    public bool HasChronicIllness { get; set; }

    // Navigation Properties
    public Family Family { get; set; } = null!;
    public OptionItem? InsuranceProvider { get; set; }

    // Many-to-Many: A health profile can have multiple checked chronic illnesses from Master Setup
    public ICollection<OptionItem> ChronicIllnesses { get; set; } = new List<OptionItem>();
}