using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BakeryApi.Infrastructure.Data
{
    public class BakeryDbContextFactory : IDesignTimeDbContextFactory<BakeryDbContext>
    {
        public BakeryDbContext CreateDbContext(string[] args)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BakeryDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new BakeryDbContext(optionsBuilder.Options);
        }
    }
}
