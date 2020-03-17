using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppTestLogging
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger = services.BuildServiceProvider().GetService<ILogger<Startup>>();
            try
            {
                services.AddControllers();
                AddClient<GoogleClient>(services, new Uri("http://www.google.com"));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error during {nameof(ConfigureServices)}");
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddClient<T>(IServiceCollection services, Uri apiAddress) where T : class
        {
            throw null;
            services
                .AddHttpClient<T>()
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = apiAddress;
                })
                .SetHandlerLifetime(TimeSpan.FromHours(1));
        }
    }
}
