using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MIS.Application.Features.Authentication;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Application.Features.Geography.Wards;
using MIS.Application.Features.Users;
using MIS.Infrastructure.Persistence.Repositories.Authentications;
using MIS.Infrastructure.Persistence.Data;
using MIS.Infrastructure.Persistence.Repositories.Geography.Municipalities;
using MIS.Infrastructure.Persistence.Repositories.Geography.Wards;
using MIS.Infrastructure.Persistence.Repositories.Users;
using Npgsql;
using MIS.Infrastructure.Identity;
using MIS.Application.Features.Geography.Toles;
using MIS.Infrastructure.Persistence.Repositories.Geography.Toles;

namespace MIS.Infrastructure;

public static class DependencyInjection
{

  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {

    // Identity
    services.AddScoped<IJwtTokenService, JwtTokenService>();
    services.AddTransient<IPasswordHashService, PasswordHashService>();


    // Repositories
    services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
    services.AddScoped<IMunicipalityRepo, MunicipalityRepo>();
    services.AddScoped<IWarrdRepo, WardRepo>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IToleRepo, ToleRepo>();


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