using Backend.Models;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services;

public class GoogleSheetsService : IDataService
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

    private static string Cell(IList<object> row, int index) =>
        (row.ElementAtOrDefault(index)?.ToString() ?? string.Empty).Trim();

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
                    Date = DateOnly.Parse(Cell(row, 0)),
                    Location = Cell(row, 1),
                    Status = Cell(row, 2),
                    Notes = row.ElementAtOrDefault(3)?.ToString()?.Trim()
                })
                .ToList() ?? [];
        }) ?? [];
    }

    public async Task<List<Location>> GetLocationsAsync()
    {
        return await _cache.GetOrCreateAsync("locations", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "Locations!A:G").ExecuteAsync();

            return response.Values?.Skip(1)
                .Where(row => row.Count >= 7)
                .Select(row => new Location
                {
                    Name = Cell(row, 0),
                    Address = Cell(row, 1),
                    City = Cell(row, 2),
                    State = Cell(row, 3),
                    Zip = Cell(row, 4),
                    Latitude = double.TryParse(Cell(row, 5), out var lat) ? lat : 0,
                    Longitude = double.TryParse(Cell(row, 6), out var lng) ? lng : 0
                })
                .ToList() ?? [];
        }) ?? [];
    }

    public async Task<List<SocialLink>> GetLinksAsync()
    {
        return await _cache.GetOrCreateAsync("links", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "Links!A:B").ExecuteAsync();

            return response.Values?.Skip(1)
                .Where(row => row.Count >= 2)
                .Select(row => new SocialLink
                {
                    Name = Cell(row, 0),
                    Url = Cell(row, 1)
                })
                .ToList() ?? [];
        }) ?? [];
    }

    public async Task<List<OtherEvent>> GetOtherEventsAsync()
    {
        return await _cache.GetOrCreateAsync("otherEvents", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "OtherEvents!A:C").ExecuteAsync();

            return response.Values?.Skip(1)
                .Where(row => row.Count >= 2
                    && DateOnly.TryParse(row[0]?.ToString(), out _))
                .Select(row => new OtherEvent
                {
                    Date = DateOnly.Parse(Cell(row, 0)),
                    Location = Cell(row, 1),
                    Notes = row.ElementAtOrDefault(2)?.ToString()?.Trim()
                })
                .ToList() ?? [];
        }) ?? [];
    }

    public async Task<SiteConfig> GetConfigAsync()
    {
        return await _cache.GetOrCreateAsync("config", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var response = await _sheetsService.Spreadsheets.Values
                .Get(_spreadsheetId, "Config!A:B").ExecuteAsync();

            var config = new SiteConfig();

            if (response.Values is null) return config;

            foreach (var row in response.Values.Skip(1).Where(r => r.Count >= 2))
            {
                var option = Cell(row, 0);
                var status = Cell(row, 1);

                if (option.Equals("Show Events?", StringComparison.OrdinalIgnoreCase))
                    config.ShowEvents = status.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
                else if (option.Equals("Show Map?", StringComparison.OrdinalIgnoreCase))
                    config.ShowMap = status.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
            }

            return config;
        }) ?? new SiteConfig();
    }
}
