using App.CheckIn.Domain.Repositories;
using App.CheckIn.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using App.CheckIn.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreServiceCollectionExtensions
    {
        /// <summary>
        /// Adds EntityFrameworkCore services, DbContexts and repositories
        /// </summary>
        /// <param name="configuration">The <see cref="DatabaseConfiguration"/> containing the ConnectionString</param>
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, DatabaseConfiguration configuration)
        {
            services.AddTnfEntityFrameworkCore();

            services.AddTnfDbContext<AppCheckInDbContext>(c => c.DbContextOptions.UseNpgsql(configuration.ConnectionString));

            services.AddTransient<IEventSubscriptionRepository, EventSubscriptionRepository>();

            return services;
        }
    }
}
