using Microsoft.EntityFrameworkCore;
using MIS.API.Models;

namespace MIS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Lookup tables
    public DbSet<OptionList> OptionLists => Set<OptionList>();
    public DbSet<OptionItem> OptionItems => Set<OptionItem>();

    // User & role management
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();

    // Submission & household
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<Household> Households => Set<Household>();
    public DbSet<Person> Persons => Set<Person>();

    // Household detail modules
    public DbSet<HhHouseInfo> HhHouseInfos => Set<HhHouseInfo>();
    public DbSet<HhFamilyInfo> HhFamilyInfos => Set<HhFamilyInfo>();
    public DbSet<HhSocioCultural> HhSocioCulturals => Set<HhSocioCultural>();
    public DbSet<HhSanitationandhygine> HhSanitationandhygines => Set<HhSanitationandhygine>();
    public DbSet<HhFamilydecision> HhFamilydecisions => Set<HhFamilydecision>();
    public DbSet<HhFamilyFacilities> HhFamilyFacilitiesList => Set<HhFamilyFacilities>();
    public DbSet<HhTransportFacility> HhTransportFacilities => Set<HhTransportFacility>();
    public DbSet<HhAgricultureInformation> HhAgricultureInformations => Set<HhAgricultureInformation>();
    public DbSet<HhProdFoodcrops> HhProdFoodcrops => Set<HhProdFoodcrops>();
    public DbSet<HhProdPulsecrops> HhProdPulsecrops => Set<HhProdPulsecrops>();
    public DbSet<HhProdOilcrops> HhProdOilcrops => Set<HhProdOilcrops>();
    public DbSet<HhVegetables> HhVegetables => Set<HhVegetables>();
    public DbSet<HhProdCashcrops> HhProdCashcrops => Set<HhProdCashcrops>();
    public DbSet<HhProdFriuts> HhProdFriuts => Set<HhProdFriuts>();
    public DbSet<HhProdLivestock> HhProdLivestocks => Set<HhProdLivestock>();
    public DbSet<HhAiLivestock> HhAiLivestocks => Set<HhAiLivestock>();
    public DbSet<HhProdBeeFishSilk> HhProdBeeFishSilks => Set<HhProdBeeFishSilk>();
    public DbSet<HhAgrovetProduction> HhAgrovetProductions => Set<HhAgrovetProduction>();
    public DbSet<HhFamilyIncome> HhFamilyIncomes => Set<HhFamilyIncome>();
    public DbSet<HhFamilyExpense> HhFamilyExpenses => Set<HhFamilyExpense>();
    public DbSet<HhDisasterInfo> HhDisasterInfos => Set<HhDisasterInfo>();
    public DbSet<HhVictimhfromnd> HhVictimhfromnds => Set<HhVictimhfromnd>();
    public DbSet<HhVictimphyfromnd> HhVictimphyfromnds => Set<HhVictimphyfromnd>();

    // Repeating groups
    public DbSet<HhAicowdetail> HhAicowdetails => Set<HhAicowdetail>();
    public DbSet<HhAibuffalodetail> HhAibuffalodetails => Set<HhAibuffalodetail>();
    public DbSet<HhAigoatdetail> HhAigoatdetails => Set<HhAigoatdetail>();
    public DbSet<HhAiswinedetail> HhAiswinedetails => Set<HhAiswinedetail>();

    // Multi-select link tables
    public DbSet<HouseholdSourceirrigation> HouseholdSourceirrigations => Set<HouseholdSourceirrigation>();
    public DbSet<HouseholdLoansource> HouseholdLoansources => Set<HouseholdLoansource>();
    public DbSet<HouseholdLoanpurpose> HouseholdLoanpurposes => Set<HouseholdLoanpurpose>();
    public DbSet<HouseholdParticipationinorg> HouseholdParticipationinorgs => Set<HouseholdParticipationinorg>();
    public DbSet<HouseholdAlterSourceOfLight> HouseholdAlterSourceOfLights => Set<HouseholdAlterSourceOfLight>();
    public DbSet<HouseholdMunIssues> HouseholdMunIssuesList => Set<HouseholdMunIssues>();
    public DbSet<PersonChildvaccine> PersonChildvaccines => Set<PersonChildvaccine>();
    public DbSet<PersonCausenotgoingschool> PersonCausenotgoingschools => Set<PersonCausenotgoingschool>();

    
}
