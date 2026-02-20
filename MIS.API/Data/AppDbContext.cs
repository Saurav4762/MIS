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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Roles & Users
        modelBuilder.Entity<AppRole>().HasKey(r => r.RoleCode);

        modelBuilder.Entity<AppUser>().HasKey(u => u.UserId);

        modelBuilder.Entity<AppUserRole>().HasKey(ur => new { ur.UserId, ur.RoleCode });
        modelBuilder.Entity<AppUserRole>()
            .HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<AppUserRole>()
            .HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleCode).OnDelete(DeleteBehavior.Cascade);

        // Option lists / items
        modelBuilder.Entity<OptionList>().HasKey(l => l.ListName);
        modelBuilder.Entity<OptionItem>().HasKey(i => new { i.ListName, i.Code });
        modelBuilder.Entity<OptionItem>()
            .HasOne(i => i.List).WithMany(l => l.OptionItems).HasForeignKey(i => i.ListName).OnDelete(DeleteBehavior.Cascade);

        // Submissions, households, persons
        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.Submissions).WithOne(s => s.CreatedByUser).HasForeignKey(s => s.CreatedBy).OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Submission>().HasKey(s => s.SubmissionId);

        modelBuilder.Entity<Household>().HasKey(h => h.HouseholdId);
        modelBuilder.Entity<Household>()
            .HasOne(h => h.Submission).WithOne(s => s.Household).HasForeignKey<Household>(h => h.SubmissionId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Person>().HasKey(p => p.PersonId);
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Household).WithMany(h => h.Persons).HasForeignKey(p => p.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        // One-to-one household detail modules (keyed by HouseholdId)
        modelBuilder.Entity<HhHouseInfo>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhHouseInfo>().HasOne(x => x.Household).WithOne(h => h.HouseInfo).HasForeignKey<HhHouseInfo>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhFamilyInfo>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhFamilyInfo>().HasOne(x => x.Household).WithOne(h => h.FamilyInfo).HasForeignKey<HhFamilyInfo>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhSocioCultural>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhSocioCultural>().HasOne(x => x.Household).WithOne(h => h.SocioCultural).HasForeignKey<HhSocioCultural>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhSanitationandhygine>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhSanitationandhygine>().HasOne(x => x.Household).WithOne(h => h.Sanitationandhygine).HasForeignKey<HhSanitationandhygine>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhFamilydecision>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhFamilydecision>().HasOne(x => x.Household).WithOne(h => h.Familydecision).HasForeignKey<HhFamilydecision>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhFamilyFacilities>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhFamilyFacilities>().HasOne(x => x.Household).WithOne(h => h.FamilyFacilities).HasForeignKey<HhFamilyFacilities>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhTransportFacility>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhTransportFacility>().HasOne(x => x.Household).WithOne(h => h.TransportFacility).HasForeignKey<HhTransportFacility>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAgricultureInformation>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhAgricultureInformation>().HasOne(x => x.Household).WithOne(h => h.AgricultureInformation).HasForeignKey<HhAgricultureInformation>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdFoodcrops>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdFoodcrops>().HasOne(x => x.Household).WithOne(h => h.ProdFoodcrops).HasForeignKey<HhProdFoodcrops>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdPulsecrops>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdPulsecrops>().HasOne(x => x.Household).WithOne(h => h.ProdPulsecrops).HasForeignKey<HhProdPulsecrops>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdOilcrops>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdOilcrops>().HasOne(x => x.Household).WithOne(h => h.ProdOilcrops).HasForeignKey<HhProdOilcrops>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhVegetables>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhVegetables>().HasOne(x => x.Household).WithOne(h => h.Vegetables).HasForeignKey<HhVegetables>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdCashcrops>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdCashcrops>().HasOne(x => x.Household).WithOne(h => h.ProdCashcrops).HasForeignKey<HhProdCashcrops>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdFriuts>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdFriuts>().HasOne(x => x.Household).WithOne(h => h.ProdFriuts).HasForeignKey<HhProdFriuts>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdLivestock>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdLivestock>().HasOne(x => x.Household).WithOne(h => h.ProdLivestock).HasForeignKey<HhProdLivestock>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAiLivestock>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhAiLivestock>().HasOne(x => x.Household).WithOne(h => h.AiLivestock).HasForeignKey<HhAiLivestock>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhProdBeeFishSilk>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhProdBeeFishSilk>().HasOne(x => x.Household).WithOne(h => h.ProdBeeFishSilk).HasForeignKey<HhProdBeeFishSilk>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAgrovetProduction>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhAgrovetProduction>().HasOne(x => x.Household).WithOne(h => h.AgrovetProduction).HasForeignKey<HhAgrovetProduction>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhFamilyIncome>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhFamilyIncome>().HasOne(x => x.Household).WithOne(h => h.FamilyIncome).HasForeignKey<HhFamilyIncome>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhFamilyExpense>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhFamilyExpense>().HasOne(x => x.Household).WithOne(h => h.FamilyExpense).HasForeignKey<HhFamilyExpense>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhDisasterInfo>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhDisasterInfo>().HasOne(x => x.Household).WithOne(h => h.DisasterInfo).HasForeignKey<HhDisasterInfo>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhVictimhfromnd>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhVictimhfromnd>().HasOne(x => x.Household).WithOne(h => h.Victimhfromnd).HasForeignKey<HhVictimhfromnd>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhVictimphyfromnd>().HasKey(x => x.HouseholdId);
        modelBuilder.Entity<HhVictimphyfromnd>().HasOne(x => x.Household).WithOne(h => h.Victimphyfromnd).HasForeignKey<HhVictimphyfromnd>(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        // Repeating groups (many)
        modelBuilder.Entity<HhAicowdetail>().HasKey(x => x.AicowdetailId);
        modelBuilder.Entity<HhAicowdetail>().HasOne(x => x.Household).WithMany(h => h.AicowDetails).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAibuffalodetail>().HasKey(x => x.AibuffalodetailId);
        modelBuilder.Entity<HhAibuffalodetail>().HasOne(x => x.Household).WithMany(h => h.AibuffaloDetails).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAigoatdetail>().HasKey(x => x.AigoatdetailId);
        modelBuilder.Entity<HhAigoatdetail>().HasOne(x => x.Household).WithMany(h => h.AigoatDetails).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HhAiswinedetail>().HasKey(x => x.AiswinedetailId);
        modelBuilder.Entity<HhAiswinedetail>().HasOne(x => x.Household).WithMany(h => h.AiswineDetails).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        // Multi-select link tables (use composite keys)
        modelBuilder.Entity<HouseholdSourceirrigation>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdSourceirrigation>().HasOne(x => x.Household).WithMany(h => h.SourceirrigationLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HouseholdLoansource>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdLoansource>().HasOne(x => x.Household).WithMany(h => h.LoansourceLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HouseholdLoanpurpose>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdLoanpurpose>().HasOne(x => x.Household).WithMany(h => h.LoanpurposeLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HouseholdParticipationinorg>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdParticipationinorg>().HasOne(x => x.Household).WithMany(h => h.ParticipationinorgLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HouseholdAlterSourceOfLight>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdAlterSourceOfLight>().HasOne(x => x.Household).WithMany(h => h.AlterSourceOfLightLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HouseholdMunIssues>().HasKey(x => new { x.HouseholdId, x.ChoiceCode });
        modelBuilder.Entity<HouseholdMunIssues>().HasOne(x => x.Household).WithMany(h => h.MunIssuesLinks).HasForeignKey(x => x.HouseholdId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PersonChildvaccine>().HasKey(x => new { x.PersonId, x.ChoiceCode });
        modelBuilder.Entity<PersonChildvaccine>().HasOne(x => x.Person).WithMany(p => p.ChildvaccineLinks).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PersonCausenotgoingschool>().HasKey(x => new { x.PersonId, x.ChoiceCode });
        modelBuilder.Entity<PersonCausenotgoingschool>().HasOne(x => x.Person).WithMany(p => p.CausenotgoingschoolLinks).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);

        // call base
        base.OnModelCreating(modelBuilder);
    }

}
