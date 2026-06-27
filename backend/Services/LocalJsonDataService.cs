using System.Text.Json;
using Backend.Models;

namespace Backend.Services;

public class LocalJsonDataService(IConfiguration configuration) : IDataService
{
    private readonly string _dataPath = configuration["LocalData:Path"] ?? "Data/Seeds/localJsonSeed.json";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public Task<List<RunEvent>> GetScheduleAsync()
    {
        var data = Load();
        return Task.FromResult(data.Schedule);
    }

    public Task<List<Location>> GetLocationsAsync()
    {
        var data = Load();
        return Task.FromResult(data.Locations);
    }

    public Task<List<SocialLink>> GetLinksAsync()
    {
        var data = Load();
        return Task.FromResult(data.Links);
    }

    public Task<List<OtherEvent>> GetOtherEventsAsync()
    {
        var data = Load();
        return Task.FromResult(data.OtherEvents);
    }

    public Task<SiteConfig> GetConfigAsync()
    {
        var data = Load();
        return Task.FromResult(data.Config);
    }

    private SeedData Load()
    {
        var json = File.ReadAllText(_dataPath);
        return JsonSerializer.Deserialize<SeedData>(json, JsonOptions) ?? new SeedData();
    }

    private class SeedData
    {
        public List<RunEvent> Schedule { get; set; } = [];
        public List<Location> Locations { get; set; } = [];
        public List<SocialLink> Links { get; set; } = [];
        public List<OtherEvent> OtherEvents { get; set; } = [];
        public SiteConfig Config { get; set; } = new();
    }
}
