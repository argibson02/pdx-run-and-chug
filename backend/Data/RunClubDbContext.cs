using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class RunClubDbContext : DbContext
{
    public RunClubDbContext(DbContextOptions<RunClubDbContext> options) : base(options) { }

    public DbSet<RunEventEntity> Schedule => Set<RunEventEntity>();
    public DbSet<LocationEntity> Locations => Set<LocationEntity>();
    public DbSet<SocialLinkEntity> Links => Set<SocialLinkEntity>();
}
