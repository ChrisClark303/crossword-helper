using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using CrosswordHelper.Data.Postgres;
using CrosswordHelper.Management.Api;
using Serilog;
using CrosswordHelper.Infrastructure.Services;
using CrosswordHelper.Data.Export;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .AddEnvironmentVariables()
                  .Build();

Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateBootstrapLogger();

Log.Information("Building web host.");

builder.Services.AddConnectionStrings(builder.Configuration);
builder.Services.AddScoped<ICrosswordHelperManagerService, CrosswordHelperManagementService>();
builder.Services.AddScoped<ICrosswordHelperManagerRepository, CrosswordHelperManagerRepository>();
builder.Services.AddTransient<IUsualSuspectDataImporter, UsualSuspectDataImporter>();
builder.Services.AddScoped<IUrlBuilder, UrlBuilder>();
builder.Services.AddScoped<IBestForPuzzlesUsualSuspectDataScraper, BestForPuzzlesUsualSuspectDataScraper>();
builder.Services.AddHttpClient<IBestForPuzzlesUsualSuspectDataScraper, BestForPuzzlesUsualSuspectDataScraper>((_, client) => client.BaseAddress = new Uri("https://bestforpuzzles.com/cryptic-crossword-dictionary/"));
builder.Services.AddScoped<ICrosswordHelperRepository, CrosswordHelperRepository>();
builder.Services.AddScoped<ICrosswordDataExtractionService, CrosswordDataExtractionService>();

builder.Services.AddCaching();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
