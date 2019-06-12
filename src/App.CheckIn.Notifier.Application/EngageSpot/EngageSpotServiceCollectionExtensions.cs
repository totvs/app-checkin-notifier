using App.CheckIn.Notifier.Application.EngageSpot;
using AppCheckInNotifier.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EngageSpotServiceCollectionExtensions
    {
        public static IServiceCollection AddEngageSpot(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EngageSpotOptions>(configuration.GetSection("EngageSpot"));
            services.AddSingleton<INotificationService, EngageSpotNotificationService>();
            services.ConfigureHttpClient<ConfigureEngageSpotHttpClient>("EngageSpot");

            return services;
        }
    }
}
