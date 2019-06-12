using App.CheckIn.EntityFrameworkCore;
using AppCheckInNotifier.Application.Hangfire;
using Hangfire;
using Hangfire.PostgreSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HangFireServiceCollectionExtensions
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services, DatabaseConfiguration databaseConfiguration)
        {
            services.AddHangfire(c =>
            {
                c.UsePostgreSqlStorage(databaseConfiguration.ConnectionString);
            });

            services.AddSingleton(new BackgroundJobServerOptions
            {
                TimeZoneResolver = new TimeZoneResolver()
            });

            services.AddHangfireServer();

            return services;
        }
    }
}
