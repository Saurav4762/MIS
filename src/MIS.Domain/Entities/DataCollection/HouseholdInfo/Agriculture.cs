using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class Agriculture : BaseEntity
{
    // Foreign Keys
    public Guid FamilyId { get; set; }
    public Guid LandUnitId { get; set; }        // For: Bigha, Kattha, Ropani, Aana
    public Guid OwnershipStatusId { get; set; }  // For: Self-Owned, Rented, Leased

    // Primitive Properties
    public decimal TotalArea { get; set; }
    public bool UsesImprovedSeeds { get; set; }     // Section 03 Toggle
    public bool UsesChemicalPesticides { get; set; } // Section 03 Toggle

    // Navigation Properties (Single Choice)
    public Family Family { get; set; } = null!;
    public OptionItem LandUnit { get; set; } = null!;
    public OptionItem OwnershipStatus { get; set; } = null!;

    // ==========================================
    // FUTURE-PROOF COLLECTIONS (Multiple Choice / Checkboxes)
    // ==========================================
    
    // Section 01: Selected Land Types (Khet, Bari, Orchard, Fallow)
    public ICollection<OptionItem> LandTypes { get; set; } = new List<OptionItem>();

    // Section 02: Selected Crop Items (Paddy, Maize, Lentils, Potato, Orange, etc.)
    public ICollection<OptionItem> SelectedCrops { get; set; } = new List<OptionItem>();

    // Section 03: Selected Machinery Equipment (Tractor, Pump Set, Thresher)
    public ICollection<OptionItem> Equipments { get; set; } = new List<OptionItem>();

    // Section 04: Selected Problems Faced (Lack of Irrigation, Fertilizer Scarcity, etc.)
    public ICollection<OptionItem> ProblemsFaced { get; set; } = new List<OptionItem>();
}