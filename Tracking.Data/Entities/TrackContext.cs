using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Data.Entities
{
    public class TrackContext :DbContext
    {
        public TrackContext(DbContextOptions<TrackContext> options) : base(options)
        {

        }
        public DbSet<Tracking> Trackings { get; set; }
    }
}
