using BakeryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Infrastructure.Data
{
    public class BakeryDbContext : DbContext
    {

        public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
: base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<ResetPassword> ResetPassword {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.email).IsUnique();
           
            modelBuilder.Entity<ResetPassword>()
                 .HasOne(r => r.user)
                    .WithMany(u => u.resetPasswords)
                        .HasForeignKey(r => r.user_id);

        }
    }
}
