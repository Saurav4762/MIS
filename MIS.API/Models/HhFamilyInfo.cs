namespace MIS.API.Models
{
    public class HhFamilyInfo
    {
        public Guid HouseholdId { get; set; }
        public string? FamilyType { get; set; }
        public int? TotalFamilyMember { get; set; }
        public string? FamilyHeadName { get; set; }
        public int? FamilyHeadAge { get; set; }
        public string? FamilyHeadGender { get; set; }
        public string? FamilyHeadContact { get; set; }
        public string? HasMigrated { get; set; }
        public string? MigrationPurpose { get; set; }
        public string? MigrationPurposeOtherText { get; set; }
        public string? MigrationDistrict { get; set; }
        public string? MigrationDistrictOtherText { get; set; }
        public string? MigrationCountry { get; set; }
        public string? MigrationCountryOtherText { get; set; }
        public string? LoanTaken { get; set; }
        public decimal? LoanAmount { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Household? Household { get; set; }
    }
}
