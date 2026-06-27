using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

var dataSource = builder.Configuration["DataSource"]?.ToLowerInvariant();
if (dataSource == "mariadb")
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDb")
        ?? throw new InvalidOperationException("ConnectionStrings:MariaDb is not configured.");
    builder.Services.AddDbContext<Backend.Data.RunClubDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    builder.Services.AddScoped<Backend.Services.IDataService, Backend.Services.MariaDbDataService>();
}
else if (dataSource == "local")
{
    builder.Services.AddSingleton<Backend.Services.IDataService, Backend.Services.LocalJsonDataService>();
}
else
{
    builder.Services.AddSingleton<Backend.Services.IDataService, Backend.Services.GoogleSheetsService>();
}

// Rate limiting: 10 requests per minute per IP
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("fixed", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));
});

// Allow the Vite dev server to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("ViteDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("ViteDev");
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();
