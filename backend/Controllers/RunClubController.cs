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
}
