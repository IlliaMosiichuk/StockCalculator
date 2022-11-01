using AppCore.Interfaces;
using AppCore.Services;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStockApiService, AlphaVantageStockApiService>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockApiService, AlphaVantageStockApiService>();
builder.Services.AddScoped<IStockDailyPriceRepository, StockDailyPriceRepository>();
builder.Services.AddSingleton<IStockPerformanceCalculator, StockPerformanceCalculator>();
builder.Services.AddSingleton<IWeekHelper, FluentDateWeekHelper>();

if (bool.TryParse(builder.Configuration["UseInMemoryDatabase"], out var useInMemoryDatabase)
    && useInMemoryDatabase)
{
    builder.Services.AddDbContext<StockCalculatorContext>(c =>
        c.UseInMemoryDatabase("StockCalculator"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<StockCalculatorContext>(options =>
        options.UseSqlServer(connectionString,
        x => x.MigrationsAssembly("Infrastructure")));
}


builder.Services.AddHttpClient("AlphaVantage", c =>
{
    c.BaseAddress = new Uri("https://www.alphavantage.co");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
