using BakeryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Infrastructure.Data
{
    public class BakeryDbContext : DbContext
    {
        public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
            : base(options) { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.email).IsUnique();
        }
    }
}
