var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

var dataSource = builder.Configuration["DataSource"]?.ToLowerInvariant();
if (dataSource == "local")
    builder.Services.AddSingleton<Backend.Services.IDataService, Backend.Services.LocalJsonDataService>();
else
    builder.Services.AddSingleton<Backend.Services.IDataService, Backend.Services.GoogleSheetsService>();

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
