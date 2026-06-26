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

    [HttpGet("bars")]
    public async Task<IActionResult> GetBars()
    {
        var bars = await _sheetsService.GetBarsAsync();
        return Ok(bars);
    }
}
