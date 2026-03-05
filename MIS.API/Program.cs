using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MIS.API.Data;
using MIS.API.Models;
using MIS.API.Repositories;
using MIS.API.Services;
using MIS.API.Exceptions;
using MIS.API.Interfaces.IServices;
using MIS.API.Configurations;
using MIS.API.Interfaces.IRepositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.Configure<JWTSettings>(
    builder.Configuration.GetSection("JWT")
);


builder.Services.AddScoped<IOptionList, OptionListRepository>();
builder.Services.AddScoped<IReligionRepo, ReligionRepo>();
builder.Services.AddScoped<IEthnicityRepo, EthnicityRepo>();
builder.Services.AddScoped<IMunicipalityRepo, MunicipalityRepo>();

builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAppRoleRepo, AppRoleRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();

// Add DbContext

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsql => npgsql.UseNetTopologySuite()
    ));

builder.Services.AddTransient<GlobalExceptionHandler>();


// Add controllers with JSON options to avoid cycles
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// Configure JWT authentication
builder.Services.AddAuthentication(options =>
    {

        options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
        options.DefaultForbidScheme =
        options.DefaultScheme =
        options.DefaultSignInScheme =
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {

        var jwtSettings = builder.Configuration.GetSection("JWT");
        var keyString = jwtSettings["Key"];

        if (string.IsNullOrEmpty(keyString))
        {
            throw new Exception("JWT Key is missing from configuration");
        }

        if (string.IsNullOrEmpty(jwtSettings["Issuer"]) || string.IsNullOrEmpty(jwtSettings["Audience"]))
        {
            Console.WriteLine("Warning: Issuer or Audience is missing. Tokens might not validate correctly.");
        }

        var key = Encoding.ASCII.GetBytes(keyString);



        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Swagger configuration with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
// Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware order
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
