using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MIS.Application.Features.Authentication;
using MIS.Application.Features.DataCollection.Agricultures;
using MIS.Application.Features.DataCollection.HouseholdInfo.Decisions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Economies;
using MIS.Application.Features.DataCollection.HouseholdInfo.Facilities;
using MIS.Application.Features.DataCollection.HouseholdInfo.Healths;
using MIS.Application.Features.DataCollection.HouseholdInfo.Livestocks;
using MIS.Application.Features.DataCollection.HouseInfo;
using MIS.Application.Features.Geography.Areas;
using MIS.Application.Features.Geography.Districts;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Application.Features.Geography.Provinces;
using MIS.Application.Features.Geography.Toles;
using MIS.Application.Features.Geography.Wards;
using MIS.Application.Features.Options.OptionItems;
using MIS.Application.Features.Options.OptionLists;
using MIS.Application.Features.Submissions;
using MIS.Application.Features.Users;
namespace MIS.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IOptionListService, OptionListService>();
    services.AddScoped<IOptionItemService, OptionItemService>();
    services.AddScoped<IMunicipalityService, MunicipalityService>();
    services.AddScoped<IDistrictService, DistrictService>();
    services.AddScoped<IWardService, WardService>();
    services.AddScoped<IToleService, ToleService>();
    services.AddScoped<IAreaService, AreaService>();
    services.AddScoped<IProvinceService, ProvinceService>();

    services.AddScoped<ISubmissionsService, SubmissionService>();


    services.AddScoped<IUserService, UserService>();

    // DATA COLLECTION
    services.AddScoped<IHouseInfoService, HouseInfoService>();
    services.AddScoped<IAgricultureService, AgricultureService>();
    services.AddScoped<IDecisionService, DecisionService>();
    services.AddScoped<IEconomyService, EconomyService>();
    services.AddScoped<IFacilityService, FacilityService>();
    services.AddScoped<ILivestockService, LivestockService>();
    services.AddScoped<IHealthService, HealthService>();
    
    // Registers LoginUserDTOValidator, RegisterUserDTOValidator, and any future validators automatically
    
    services.AddValidatorsFromAssemblyContaining<LoginUserDTOValidator>();
    services.AddValidatorsFromAssemblyContaining<CreateUserDTOValidator>();

    services.AddValidatorsFromAssemblyContaining<CreateOptionListDTOValidator>();

    services.AddValidatorsFromAssemblyContaining<CreateOptionItemDTOValidator>();

    return services;
  }
}