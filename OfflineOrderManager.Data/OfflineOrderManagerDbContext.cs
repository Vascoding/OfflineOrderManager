using Microsoft.EntityFrameworkCore;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Data.Users;

namespace OfflineOrderManager.Data
{
    public class OfflineOrderManagerDbContext : DbContext
    {
        public OfflineOrderManagerDbContext(DbContextOptions<OfflineOrderManagerDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(u => new { u.Id });

            builder.Entity<Order>()
                .HasKey(o => o.Id);

            builder.Entity<Order>()
                .HasOne<User>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            base.OnModelCreating(builder);
        }
    }
}
