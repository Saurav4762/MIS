public class AgricultureDTO
{
    public Guid Id { get; set;}
    public Guid FamilyId { get; set; }
    public Guid LandUnitId { get; set; }
    public Guid OwnershipStatusId { get; set; }
    public decimal TotalArea { get; set; }
    public bool UsesImprovedSeeds { get; set; }
    public bool UsesChemicalPesticides { get; set; }

    public List<AgricultureCropDTO> SelectedCrops { get; set; } = new();
    public List<AgricultureEquipmentDTO> Equipments { get; set; } = new();
    public List<AgricultureLandTypeDTO> LandTypes {get; set;} = new();
    public List<AgricultureProblemDTO> ProblemsFaced { get; set; } = new();
}