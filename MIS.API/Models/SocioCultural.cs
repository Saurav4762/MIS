namespace MIS.API.Models;

public class SocioCultural
{
    public Guid Id { get; set; }
    public Guid HouseholdId { get; set; }
    
    public Guid EthnicityId { get; set; }
    public OptionItem Ethnicity { get; set; }
    public string? EthnicityOtherText { get; set; }
    
    public Guid ReligionId { get; set; }
    public OptionItem Religion { get; set; }
    public string? ReligionOtherText { get; set; }
    
    public Guid MothertongueId { get; set; }
    public OptionItem MotherTongue { get; set; }
    public string? MotherTongueOtherText  { get; set; }
    
    public Guid ComLanguageId { get; set; }
    public OptionItem ComLanguage { get; set; }
    public string? ComLanguageOtherText { get; set; }
    
    public Guid MajorFamilyOccupationId { get; set; }
    public OptionItem MajorFamilyOccupation { get; set; }
    public string? MajorFamilyOccupationOtherText { get; set; }
    
    public string SettlementType { get; set; }
    public string FamilyLandArea { get; set; }
    public string FoodSufficiency { get; set; }
    public int ChildUnder5 { get; set; }
    public int Child5to14 { get; set; }
    public int Youth15to29 { get; set; }
    public int Adult30to59  { get; set; }
    public int Senior60Plus { get; set; }
    public DateTime UpdatedAt { get; set; } =DateTime.UtcNow;

// Navigation
    public Household Household { get; set; }


}