using CrosswordHelper.Api;
using CrosswordHelper.Data;
using CrosswordHelper.Data.Postgres;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICrosswordHelperService,CrosswordHelperService>();
builder.Services.AddScoped<ICrosswordHelperRepository, CrosswordHelperRepository>();
builder.Services.AddScoped<ICrosswordHelperManagerService, CrosswordHelperManagementService>();
builder.Services.AddScoped<ICrosswordHelperManagerRepository, CrosswordHelperManagerRepository>();

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

app.MapPost("/help/anagram-indicators/{word}", (string word, [FromServices] ICrosswordHelperManagerService helperService) =>
{
    helperService.AddAnagramIndictor(word);
});
app.MapPost("/help/container-indicators/{word}", (string word, [FromServices] ICrosswordHelperManagerService helperService) =>
{
    helperService.AddContainerIndicator(word);
});
app.MapPost("/help/reversal-indicators/{word}", (string word, [FromServices] ICrosswordHelperManagerService helperService) =>
{
    helperService.AddReversalIndicator(word);
});
app.Run();