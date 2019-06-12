using AppCheckInNotifier.Application.FirebaseCloudMessaging;
using AppCheckInNotifier.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FCMServiceCollecitonExtensions
    {
        public static IServiceCollection AddFirebaseCloudMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseCloudMessagingOptions>(configuration.GetSection("FireBase"));
            services.AddSingleton<INotificationService, FirebaseNotificationService>();

            return services;
        }
    }
}
