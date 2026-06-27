using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class RunClubDbContext(DbContextOptions<RunClubDbContext> options) : DbContext(options)
{

    public static DbSet<RunEventEntity> Schedule => Set<RunEventEntity>();
    public static DbSet<LocationEntity> Locations => Set<LocationEntity>();
    public static DbSet<SocialLinkEntity> Links => Set<SocialLinkEntity>();
}
