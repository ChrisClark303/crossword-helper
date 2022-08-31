using CrosswordHelper.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICrosswordHelperService,CrosswordHelperService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/help/{crosswordClue}", (string crosswordClue,[FromServices]ICrosswordHelperService helperService) =>
{
    return helperService.Help(crosswordClue);
})
.WithName("GetCrosswordHelp");

app.Run();