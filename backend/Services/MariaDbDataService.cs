using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class MariaDbDataService(RunClubDbContext db) : IDataService
{
    public async Task<List<RunEvent>> GetScheduleAsync()
    {
        return await db.Schedule
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
        return await db.Locations
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
        return await db.Links
            .Select(l => new SocialLink
            {
                Name = l.Name,
                Url = l.Url
            })
            .ToListAsync();
    }
}
