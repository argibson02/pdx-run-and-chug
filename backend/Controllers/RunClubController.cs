using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RunClubController : ControllerBase
{
    private readonly IDataService _dataService;

    public RunClubController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> GetSchedule()
    {
        var schedule = await _dataService.GetScheduleAsync();
        return Ok(schedule);
    }

    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _dataService.GetLocationsAsync();
        return Ok(locations);
    }

    [HttpGet("links")]
    public async Task<IActionResult> GetLinks()
    {
        var links = await _dataService.GetLinksAsync();
        return Ok(links);
    }
}
