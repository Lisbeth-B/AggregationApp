using AggregationApp.Core;
using AggregationApp.Core.Filter;
using AggregationApp.Infra.CsvHelpers;
using AggregationApp.Infra.Http;
using AggregationApp.Infra.Models;
using AggregationApp.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

builder.Services.AddDbContext<AggregationAppContext>(options =>
{
    options.UseSqlServer($"Server={dbHost},{dbPort};Initial Catalog={dbName};User ID=sa;Password={dbPassword}; Encrypt=False;");
});

builder.Services.AddScoped<ICSVReader, CSVReader>();
builder.Services.AddHttpClient<ICSVReader, CSVReader>();
builder.Services.AddScoped<IElectricityConsumptionRepository, ElectricityConsumptionRepository>();
builder.Services.AddScoped<IDataDownloaderService, DataDownloaderService>();
builder.Services.AddLogging();
builder.Services.AddScoped<IElectricityDataAggregator, ElectricityDataAggregator>();


WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();