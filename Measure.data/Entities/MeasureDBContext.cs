using Microsoft.EntityFrameworkCore;

namespace Measure.Data.Entities
{
    public class MeasureDBContext :DbContext
    {
        public MeasureDBContext(DbContextOptions<MeasureDBContext> options) : base(options)
        {

        }
        public DbSet<Measure> Measures { get; set; }
        
    }
}
