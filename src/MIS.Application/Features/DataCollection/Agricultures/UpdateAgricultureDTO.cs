public class UpdateAgricultureDTO
{
    public Guid? LandUnitId { get; set; }
    public Guid? OwnershipStatusId { get; set; }
    public decimal? TotalArea { get; set; }
    public bool? UsesImprovedSeeds { get; set; }
    public bool? UsesChemicalPesticides { get; set; }

    public List<AgricultureCropDTO>? SelectedCrops { get; set; } 
    public List<AgricultureEquipmentDTO>? Equipments { get; set; } 
    public List<AgricultureLandTypeDTO>? LandTypes { get; set; } 
    public List<AgricultureProblemDTO>? ProblemsFaced { get; set; } 
}