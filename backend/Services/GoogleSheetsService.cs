using Backend.Models;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services;

public class GoogleSheetsService
{
    private readonly SheetsService _sheetsService;
    private readonly string _spreadsheetId;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    public GoogleSheetsService(IConfiguration configuration, IMemoryCache cache)
    {
        _cache = cache;
        _spreadsheetId = configuration["GoogleSheets:SpreadsheetId"]
            ?? throw new InvalidOperationException("GoogleSheets:SpreadsheetId is not configured.");

        var apiKey = configuration["GoogleSheets:ApiKey"]
            ?? throw new InvalidOperationException("GoogleSheets:ApiKey is not configured.");

        _sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            ApiKey = apiKey,
            ApplicationName = "PdxRunAndChug"
        });
    }

    public async Task<List<RunEvent>> GetScheduleAsync()
    {
        return await _cache.GetOrCreateAsync("schedule", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "Schedule!A:D").ExecuteAsync();

            return response.Values?.Skip(1)
                .Where(row => row.Count >= 2
                    && DateOnly.TryParse(row[0]?.ToString(), out _))
                .Select(row => new RunEvent
                {
                    Date = DateOnly.Parse(row[0]!.ToString()!),
                    Location = row.ElementAtOrDefault(1)?.ToString() ?? string.Empty,
                    Status = row.ElementAtOrDefault(2)?.ToString() ?? string.Empty,
                    Notes = row.ElementAtOrDefault(3)?.ToString()
                })
                .ToList() ?? [];
        }) ?? [];
    }

    public async Task<List<Bar>> GetBarsAsync()
    {
        return await _cache.GetOrCreateAsync("bars", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "Bars!A:G").ExecuteAsync();

            return response.Values?.Skip(1)
                .Where(row => row.Count >= 7)
                .Select(row => new Bar
                {
                    Name = row[0]?.ToString() ?? string.Empty,
                    Address = row[1]?.ToString() ?? string.Empty,
                    City = row[2]?.ToString() ?? string.Empty,
                    State = row[3]?.ToString() ?? string.Empty,
                    Zip = row[4]?.ToString() ?? string.Empty,
                    Latitude = double.TryParse(row[5]?.ToString(), out var lat) ? lat : 0,
                    Longitude = double.TryParse(row[6]?.ToString(), out var lng) ? lng : 0
                })
                .ToList() ?? [];
        }) ?? [];
    }
}
