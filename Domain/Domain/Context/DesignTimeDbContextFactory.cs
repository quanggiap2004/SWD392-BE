using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Domain.Domain.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlindBoxSystemDbContext>
    {
        public BlindBoxSystemDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\APILayer");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BlindBoxSystemDbContext>();
            var connectionString = configuration.GetConnectionString("connection");
            optionsBuilder.UseNpgsql(connectionString);

            return new BlindBoxSystemDbContext(optionsBuilder.Options);
        }
    }
}
