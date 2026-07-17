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
    
    // Replace with:
    public ICollection<AgricultureLandType> LandTypes { get; set; } = new List<AgricultureLandType>();
    public ICollection<AgricultureCrop> SelectedCrops { get; set; } = new List<AgricultureCrop>();
    public ICollection<AgricultureEquipment> Equipments { get; set; } = new List<AgricultureEquipment>();
    public ICollection<AgricultureProblem> ProblemsFaced { get; set; } = new List<AgricultureProblem>();

}