using System;
using App.CheckIn.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppCheckInNotifier
{
    public class Startup
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public Startup(IConfiguration configuration)
        {
            _databaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(_databaseConfiguration);

            services.AddTnfAspNetCore();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseTnfAspNetCore(c =>
            {
                c.ConfigureApplicationLocalization();
            });
        }
    }
}
