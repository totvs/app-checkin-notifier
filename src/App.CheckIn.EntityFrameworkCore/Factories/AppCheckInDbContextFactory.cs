using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tnf.Runtime.Session;

namespace App.CheckIn.EntityFrameworkCore.Factories
{
    public class AppCheckInDbContextFactory : IDesignTimeDbContextFactory<AppCheckInDbContext>
    {
        public AppCheckInDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.Development.json", false)
                    .AddEnvironmentVariables()
                    .Build();

            var builder = new DbContextOptionsBuilder<AppCheckInDbContext>();

            builder.UseNpgsql(configuration["ConnectionString"]);

            return new AppCheckInDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
