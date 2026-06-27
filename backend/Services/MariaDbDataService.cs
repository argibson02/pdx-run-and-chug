using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class MariaDbDataService : IDataService
{
    private readonly RunClubDbContext _db;

    public MariaDbDataService(RunClubDbContext db)
    {
        _db = db;
    }

    public async Task<List<RunEvent>> GetScheduleAsync()
    {
        return await _db.Schedule
            .OrderBy(e => e.Date)
            .Select(e => new RunEvent
            {
                Date = e.Date,
                Location = e.Location,
                Status = e.Status,
                Notes = e.Notes
            })
            .ToListAsync();
    }

    public async Task<List<Location>> GetLocationsAsync()
    {
        return await _db.Locations
            .OrderBy(l => l.Name)
            .Select(l => new Location
            {
                Name = l.Name,
                Address = l.Address,
                City = l.City,
                State = l.State,
                Zip = l.Zip,
                Latitude = l.Latitude,
                Longitude = l.Longitude
            })
            .ToListAsync();
    }

    public async Task<List<SocialLink>> GetLinksAsync()
    {
        return await _db.Links
            .Select(l => new SocialLink
            {
                Name = l.Name,
                Url = l.Url
            })
            .ToListAsync();
    }
}
