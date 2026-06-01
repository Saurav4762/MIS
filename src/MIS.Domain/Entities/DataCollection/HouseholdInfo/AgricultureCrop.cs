using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class AgricultureCrop : BaseEntity
{
    public Guid AgricultureId { get; set; }      // FK to Agriculture
    public Guid CropId { get; set; }             // FK to OptionItem (the crop type)

    // Optional extra fields (example)
    public decimal? AreaInHectares { get; set; }
    public decimal? EstimatedYield { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Agriculture Agriculture { get; set; } = null!;
    public OptionItem Crop { get; set; } = null!;
}