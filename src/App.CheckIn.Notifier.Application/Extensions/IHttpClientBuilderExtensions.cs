using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IHttpClientBuilderExtensions
    {
        public static IServiceCollection ConfigureHttpClient<TConfigureHttpClient>(this IServiceCollection services, string name)
            where TConfigureHttpClient : class, IConfigureHttpClient

        {
            services.AddHttpClient(name)
                .ConfigureHttpClient<TConfigureHttpClient>();

            return services;
        }

        public static IHttpClientBuilder ConfigureHttpClient<THttpClientConfigurer>(this IHttpClientBuilder builder)
            where THttpClientConfigurer : class, IConfigureHttpClient
        {
            builder.Services.AddTransient<THttpClientConfigurer>();

            builder.Services.AddOptions<HttpClientFactoryOptions>(builder.Name)
                .Configure<THttpClientConfigurer>((options, configurer) => options.HttpClientActions.Add(configurer.Configure));

            return builder;
        }
    }
}
