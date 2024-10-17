using CrosswordHelper.Api;
using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using CrosswordHelper.Data.Postgres;
using Microsoft.AspNetCore.Mvc;
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

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionStrings>(connStrings!);
builder.Services.AddScoped<ICrosswordHelperService,CrosswordHelperService>();
builder.Services.AddScoped<ICrosswordHelperRepository, CrosswordHelperRepository>();
builder.Services.AddScoped<ICrosswordHelperManagerService, CrosswordHelperManagementService>();
builder.Services.AddScoped<ICrosswordHelperManagerRepository, CrosswordHelperManagerRepository>();
builder.Services.AddTransient<IUsualSuspectDataImporter, UsualSuspectDataImporter>();
builder.Services.AddControllers();

AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);

var app = builder.Build();

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