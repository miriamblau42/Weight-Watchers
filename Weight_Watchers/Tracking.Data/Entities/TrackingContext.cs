using Microsoft.EntityFrameworkCore;

namespace Tracking.Data.Entities;

public class TrackingContext: DbContext
{
    public TrackingContext(DbContextOptions<TrackingContext> options) : base(options) {    }
    public DbSet<Track> Tracks { get; set; }
}
    