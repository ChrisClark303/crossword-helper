using CrosswordHelper.Api;
using CrosswordHelper.Api.Models;
using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using CrosswordHelper.Data.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .AddEnvironmentVariables()
                  .Build();

Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateBootstrapLogger();

Log.Information("Building web host.");

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Host.UseSerilog((ctx, services, config) =>
{
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services);
});

var connStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
var apiKeySettings = builder.Configuration.GetSection("ApiKeySettings").Get<ApiKeySettings>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "X-Api-Key", //header with api key
        Type = SecuritySchemeType.ApiKey,
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { key, new List<string>(){ } }
    };

    c.AddSecurityRequirement(requirement);
});
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionStrings>(connStrings!);
builder.Services.AddSingleton<ApiKeySettings>(apiKeySettings!);
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ICrosswordHelperService,CrosswordHelperService>();
builder.Services.AddScoped<ICrosswordHelperRepository, CrosswordHelperRepository>();
builder.Services.AddScoped<ICrosswordHelperManagerService, CrosswordHelperManagementService>();
builder.Services.AddScoped<ICrosswordHelperManagerRepository, CrosswordHelperManagerRepository>();
builder.Services.AddTransient<IUsualSuspectDataImporter, UsualSuspectDataImporter>();
builder.Services.AddControllers();

AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.BuildRoutes();
app.MapControllers();

app.Run();