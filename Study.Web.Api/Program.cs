using System.ComponentModel;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSerilog(configure => configure.ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => { options.WithTitle("Study.Web.Api"); });
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options => options.IncludeQueryInRequestPath = true);

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithDescription("Get weather forecast");

app.Run();

record WeatherForecast(
    [property: Description("The forecast date")] DateOnly Date,
    [property: Description("The temperature in Celsius")] int TemperatureC,
    [property: Description("The summary")] string? Summary)
{
    [Description("The temperature in Fahrenheit")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}