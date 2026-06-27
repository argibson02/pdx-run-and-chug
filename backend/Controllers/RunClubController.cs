using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RunClubController(IDataService dataService) : ControllerBase
{
    [HttpGet("schedule")]
    public async Task<IActionResult> GetSchedule()
    {
        var schedule = await dataService.GetScheduleAsync();
        return Ok(schedule);
    }

    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await dataService.GetLocationsAsync();
        return Ok(locations);
    }

    [HttpGet("links")]
    public async Task<IActionResult> GetLinks()
    {
        var links = await dataService.GetLinksAsync();
        return Ok(links);
    }

    [HttpGet("other-events")]
    public async Task<IActionResult> GetOtherEvents()
    {
        var events = await dataService.GetOtherEventsAsync();
        return Ok(events);
    }

    [HttpGet("config")]
    public async Task<IActionResult> GetConfig()
    {
        var config = await dataService.GetConfigAsync();
        return Ok(config);
    }
}
