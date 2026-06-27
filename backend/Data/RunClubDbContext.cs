using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class RunClubDbContext(DbContextOptions<RunClubDbContext> options) : DbContext(options)
{

    public DbSet<RunEventEntity> Schedule { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<SocialLinkEntity> Links { get; set; }
}
