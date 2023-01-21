using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Subscriber.Data.Entities;

namespace Measure.Data.Entities;

public class MeasureContext: DbContext
{

    public MeasureContext(DbContextOptions<MeasureContext> options) : base(options) { }
    public DbSet<Measure> Measures { get; set; }
    
}
