using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MIS.Application.Common.Interfaces;
using MIS.Application.Features.Authentication;
using MIS.Application.Features.DataCollection.Agricultures;
using MIS.Application.Features.Geography.Districts;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Application.Features.Geography.Provinces;
using MIS.Application.Features.Geography.Wards;
using MIS.Application.Features.Users;
using MIS.Infrastructure.Persistence.Repositories.Authentications;
using MIS.Infrastructure.Persistence.Data;
using MIS.Infrastructure.Persistence.Repositories.Geography.Municipalities;
using MIS.Infrastructure.Persistence.Repositories.Geography.Districts;
using MIS.Infrastructure.Persistence.Repositories.Geography.Provinces;
using MIS.Infrastructure.Persistence.Repositories.Geography.Wards;
using MIS.Infrastructure.Persistence.Repositories.Users;
using Npgsql;
using MIS.Infrastructure.Identity;
using MIS.Application.Features.Geography.Toles;
using MIS.Infrastructure.Persistence.Repositories.Geography.Toles;
using OfficeOpenXml;
using MIS.Infrastructure.ExcelParser;
using MIS.Application.Features.Options.OptionLists;
using MIS.Application.Features.Options.OptionItems;
using MIS.Infrastructure.Persistence.Repositories.Options;
using MIS.Application.Features.Geography.Areas;
using MIS.Infrastructure.Persistence.Repositories.Geography.Areas;
using MIS.Application.Features.Submissions;
using MIS.Infrastructure.Persistence.Repositories.Submissions;
using MIS.Application.Features.DataCollection.HouseInfo;
using MIS.Infrastructure.Persistence.Repositories.DataCollection;
using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Economies;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;
using MIS.Application.Features.DataCollection.HouseholdInfo.Healths;
using MIS.Infrastructure.Persistence;
using MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;

namespace MIS.Infrastructure;

public static class DependencyInjection
{

  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    ExcelPackage.License.SetNonCommercialPersonal("Your Name");

    // Excel Parser
    services.AddTransient<IMunicipalityExcelParser, MunicipalityExcelParser>();


    // Identity
    services.AddScoped<IJwtTokenService, JwtTokenService>();
    services.AddTransient<IPasswordHashService, PasswordHashService>();


    // Repositories
    services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
    services.AddScoped<IOptionListRepo, OptionListRepo>();
    services.AddScoped<IOptionItemRepo, OptionItemRepo>();

    services.AddScoped<IMunicipalityRepo, MunicipalityRepo>();
    services.AddScoped<IDistrictRepo, DistrictRepo>();
    services.AddScoped<IProvinceRepo, ProvinceRepo>();
    services.AddScoped<IWardRepo, WardRepo>();
    services.AddScoped<IAreaRepo, AreaRepo>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IToleRepo, ToleRepo>();

    // Submissions
    services.AddScoped<ISubmissionsRepo, SubmissionsRepo>();

    // Data Collection
    services.AddScoped<IHouseInfoRepo, HouseInfoRepo>();
    services.AddScoped<IAgricultureRepo, AgricultureRepo>();
    services.AddScoped<IDecisionRepo, DecisionsRepo>();
    services.AddScoped<IEconomyRepo, EconomyRepo>();
    services.AddScoped<IFacilityRepo, FacilityRepo>();
    services.AddScoped<ILivestockRepo, LivestockRepo>();
    services.AddScoped<IHealthRepo, HealthRepo>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();


    // Data
    // Enable Dynamic Serialization
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(
        configuration.GetConnectionString("DefaultConnection")
    );

    var dataSource = dataSourceBuilder.EnableDynamicJson().Build();


    // Add DbContext
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            dataSource,
            npgsql => npgsql.UseNetTopologySuite()
        ));


    return services;
  }
}