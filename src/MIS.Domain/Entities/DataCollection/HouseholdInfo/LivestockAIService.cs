using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class LivestockAIService : BaseEntity
{
    // Link back to the parent Livestock entity
    public Guid LivestockId { get; set; }
    
    // Form Inputs & Master Setup Lookup Foreign Keys
    public Guid AnimalTypeId { get; set; }      // From dropdown lookup (e.g., Cow, Buffalo)
    public string AnimalName { get; set; } = null!; // Text entry for custom animal designation
    public int AgeYears { get; set; }            // Age numeric entry
    public Guid BirthHistoryId { get; set; }    // Toggle/Dropdown lookup (e.g., Natural, Inseminated)
    public string SemenOrBullName { get; set; } = null!; // Text entry for breeding specifications
    public string AiServiceDate { get; set; } = null!;  // String mapping to natively handle Nepali BS date entries safely
    public Guid StatusId { get; set; }          // Dropdown lookup tracking results (e.g., Pregnant, Failed)

    // Navigation Properties
    public Livestock Livestock { get; set; } = null!;
    public OptionItem AnimalType { get; set; } = null!;
    public OptionItem BirthHistory { get; set; } = null!;
    public OptionItem Status { get; set; } = null!;
}