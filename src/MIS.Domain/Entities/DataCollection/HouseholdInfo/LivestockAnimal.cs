using MIS.Domain.Common.Premitives;

namespace MIS.Domain.Entities.DataCollection.HouseholdInfo;

public class LivestockAnimal : BaseEntity
{
    // Link back to the parent Livestock entity
    public Guid LivestockId { get; set; }
    
    // Link to the dynamic Master Setup OptionItem (e.g., Cow, Buffalo, Goat, Pig)
    public Guid AnimalTypeId { get; set; } 
    
    // The number field tracking the current quantity count
    public int Count { get; set; }

    // Navigation Properties
    public Livestock Livestock { get; set; } = null!;
    public OptionItem AnimalType { get; set; } = null!;
}