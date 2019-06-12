using App.CheckIn.EntityFrameworkCore;
using AppCheckInNotifier;
using AppCheckInNotifier.Application.Localization;
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
            services.AddFirebaseCloudMessaging(databaseConfiguration.Configuration);
            services.AddEngageSpot(databaseConfiguration.Configuration);
            services.AddHangFire(databaseConfiguration);

            services.AddHostedService<ApplicationJobsRegistrar>();

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
