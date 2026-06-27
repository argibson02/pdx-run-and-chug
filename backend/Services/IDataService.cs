using Backend.Models;

namespace Backend.Services;

public interface IDataService
{
    Task<List<RunEvent>> GetScheduleAsync();
    Task<List<Location>> GetLocationsAsync();
    Task<List<SocialLink>> GetLinksAsync();
    Task<List<OtherEvent>> GetOtherEventsAsync();
    Task<SiteConfig> GetConfigAsync();
}
