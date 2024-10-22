using DotskinWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotskinWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи "многие ко многим" между User и Product
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithMany(p => p.Users);
        }
    }
}
