using Microsoft.EntityFrameworkCore;

namespace OfflineOrderManager.Data
{
    public class OfflineOrderManagerDbContext : DbContext
    {
        public OfflineOrderManagerDbContext(DbContextOptions<OfflineOrderManagerDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
