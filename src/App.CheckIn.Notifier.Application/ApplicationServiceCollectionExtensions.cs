using App.CheckIn.EntityFrameworkCore;
using AppCheckInNotifier;
using AppCheckInNotifier.Application.FirebaseCloudMessaging;
using AppCheckInNotifier.Application.Hangfire;
using AppCheckInNotifier.Application.Localization;
using AppCheckInNotifier.Application.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="databaseConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services, DatabaseConfiguration databaseConfiguration)
        {
            services.AddEntityFrameworkCore(databaseConfiguration);

            services.AddHangfire(c =>
            {
                c.UsePostgreSqlStorage(databaseConfiguration.ConnectionString);
            });

            services.Configure<FirebaseCloudMessagingOptions>(databaseConfiguration.Configuration);

            services.AddSingleton(new BackgroundJobServerOptions
            {
                TimeZoneResolver = new TimeZoneResolver()
            });

            services.AddHostedService<ApplicationJobsRegistrar>();
            services.AddHangfireServer();

            services.AddSingleton<INotificationService, FirebaseNotificationService>();

            return services;
        }

        /// <summary>
        /// Configures localization for the application
        /// </summary>
        public static ITnfConfiguration ConfigureApplicationLocalization(this ITnfConfiguration configuration)
        {
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));

            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(LocalizationSources.Application,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ApplicationServiceCollectionExtensions).Assembly,
                        "App.CheckIn.Notifier.Application.Localization.SourceFiles"
                    )
                )
            );

            return configuration;
        }
    }
}
