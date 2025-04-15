using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PublisherData
{
    public class DesignTimePubContextFactory : IDesignTimeDbContextFactory<PubContext>
    {
        public PubContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PubContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("PubConnection"));

            return new PubContext(optionsBuilder.Options);
        }
    }
} 