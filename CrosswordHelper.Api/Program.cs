using CrosswordHelper.Api;
using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using CrosswordHelper.Data.Postgres;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<ICrosswordHelperService,CrosswordHelperService>();
builder.Services.AddScoped<ICrosswordHelperRepository, CrosswordHelperRepository>();
builder.Services.AddScoped<ICrosswordHelperManagerService, CrosswordHelperManagementService>();
builder.Services.AddScoped<ICrosswordHelperManagerRepository, CrosswordHelperManagerRepository>();
builder.Services.AddTransient<IUsualSuspectDataImporter, UsualSuspectDataImporter>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.BuildRoutes();
app.MapControllers();

app.Run();