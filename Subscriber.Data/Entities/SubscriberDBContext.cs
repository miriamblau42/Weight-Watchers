using Microsoft.EntityFrameworkCore;


namespace Subscriber.Data.Entities
{
    public class SubscribeDBContext :DbContext
    {
        public SubscribeDBContext(DbContextOptions<SubscribeDBContext> options) : base(options)
        {
            
        }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }

    }
}
