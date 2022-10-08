using CrosswordHelper.Api;
using CrosswordHelper.Data;
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

app.MapGet("/help/{crosswordClue}", (string crosswordClue,[FromServices]ICrosswordHelperService helperService) =>
{
    return helperService.Help(crosswordClue);
})
.WithName("GetCrosswordHelp");
app.MapGet("/help/anagram-indicators", ([FromServices] ICrosswordHelperService helperService) =>
{
    return helperService.GetAnagramIndicators();
})
.WithName("GetAnagramIndicators");
app.MapGet("/help/removal-indicators", ([FromServices] ICrosswordHelperService helperService) =>
{
    return helperService.GetRemovalIndicators();
})
.WithName("GetRemovalIndicators");
app.MapGet("/help/reversal-indicators", ([FromServices] ICrosswordHelperService helperService) =>
{
    return helperService.GetReversalIndicators();
})
.WithName("GetReversalIndicators");
app.MapGet("/help/container-indicators", ([FromServices] ICrosswordHelperService helperService) =>
{
    return helperService.GetContainerIndicators();
})
.WithName("GetContainerIndicators");
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
app.MapPost("/help/removal-indicators/{word}", (string word, [FromServices] ICrosswordHelperManagerService helperService) =>
{
    helperService.AddRemovalIndicator(word);
});
app.Run();