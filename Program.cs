using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add MySQL configuration
var connectionString = "Server=localhost;Database=MyWebService;User=webapp_user;Password=admin;";
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Create and save a new forecast
app.MapPost("/weatherforecast", async (WeatherDbContext db) =>
{
    var forecast = new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = summaries[Random.Shared.Next(summaries.Length)]
    };
    
    db.WeatherForecasts.Add(forecast);
    await db.SaveChangesAsync();
    return Results.Created($"/weatherforecast/{forecast.Date}", forecast);
});

// Get all forecasts
app.MapGet("/weatherforecast", async (WeatherDbContext db) =>
{
    return await db.WeatherForecasts.ToListAsync();
});

// Get forecast by date
app.MapGet("/weatherforecast/{date}", async (DateOnly date, WeatherDbContext db) =>
{
    return await db.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == date);
});

app.Run();

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}

public record WeatherForecast
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
