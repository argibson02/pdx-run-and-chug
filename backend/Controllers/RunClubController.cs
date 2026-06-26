using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RunClubController : ControllerBase
{
    private readonly GoogleSheetsService _sheetsService;

    public RunClubController(GoogleSheetsService sheetsService)
    {
        _sheetsService = sheetsService;
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> GetSchedule()
    {
        var schedule = await _sheetsService.GetScheduleAsync();
        return Ok(schedule);
    }

    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _sheetsService.GetLocationsAsync();
        return Ok(locations);
    }

    [HttpGet("links")]
    public async Task<IActionResult> GetLinks()
    {
        var links = await _sheetsService.GetLinksAsync();
        return Ok(links);
    }
}
