using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MIS.API.Common.Middlewares;
using MIS.API.Common.Responses;
using MIS.Application;
using MIS.Application.Features.Authentication;
using MIS.Infrastructure;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter 'Bearer' [space] and then your token"
  });
  c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
  {
    {
        new OpenApiSecuritySchemeReference("Bearer", document),
        new List<string>()
    }
  });
});


var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
          ?? throw new InvalidOperationException("Jwt settings are missing.");

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));


builder.Services.AddAuthorization();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.DefaultIgnoreCondition =
          System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    }).ConfigureApiBehaviorOptions(options =>
    {
      options.SuppressModelStateInvalidFilter = false;
      options.InvalidModelStateResponseFactory = context =>
      {
        var details = context.ModelState
          .Where(e => e.Value?.Errors.Count > 0)
          .ToDictionary(
              kvp =>
              {
                var key = kvp.Key;

                // Remove "$." prefix from JSON path keys like "$.areaId"
                if (key.StartsWith("$."))
                  key = key[2..];

                return key;
              },
              kvp => kvp.Value!.Errors
                  .Select(e => e.ErrorMessage)
                  .ToArray()
          );



        var apiError = new ApiError
        {
          Code = "VALIDATION_ERROR",
          Message = "One or more validation errors occurred.",
          Details = details
        };

        var apiResponse = new ApiResponse<object>
        {
          Success = false,
          Message = "Validation failed",
          Error = apiError,
          StatusCode = System.Net.HttpStatusCode.BadRequest
        };

        return new BadRequestObjectResult(apiResponse);
      };
    });


builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
  });
});


builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidateLifetime = true,
      ValidIssuer = jwt.Issuer,
      ValidAudience = jwt.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
      ClockSkew = TimeSpan.Zero
    };
  });



var app = builder.Build();




app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

if (!app.Environment.IsDevelopment())
{
  app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();




if (app.Environment.IsDevelopment())
{
  app.MapGet("/debug-routes", (EndpointDataSource ds) =>
    string.Join("\n", ds.Endpoints.Select(e => e.DisplayName)));
}

app.MapGet("/ping", () =>
{
  return Results.Ok(new { message = "pong" });
});



app.Run();

