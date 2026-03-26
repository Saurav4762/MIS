using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MIS.Application.Features.Authentication;
using MIS.Application.Features.Geography.Municipalities;
using MIS.Application.Features.Geography.Toles;
using MIS.Application.Features.Geography.Wards;
using MIS.Application.Features.Options.OptionItems;
using MIS.Application.Features.Options.OptionLists;
using MIS.Application.Features.Users;

namespace MIS.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IMunicipalityService, MunicipalityService>();
    services.AddScoped<IWardService, WardService>();
    services.AddScoped<IToleService, ToleService>();


    services.AddScoped<IUserService, UserService>();

    // Registers LoginUserDTOValidator, RegisterUserDTOValidator, and any future validators automatically
    services.AddValidatorsFromAssemblyContaining<LoginUserDTOValidator>();
    services.AddValidatorsFromAssemblyContaining<CreateUserDTOValidator>();

    services.AddValidatorsFromAssemblyContaining<CreateOptionListDTOValidator>();

    services.AddValidatorsFromAssemblyContaining<CreateOptionItemDTOValidator>();

    return services;
  }
}