using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.AspNetCore;

namespace BasicProject.Api
{
    /// <summary>
    ///     Provides the application configuration at start up.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        
        /// <summary>
        ///     Constructs a new instance of <see cref="Startup" />.
        /// </summary>
        /// <param name="configuration">An <see cref="IConfiguration" /> representing the application's configuration.</param>
        /// <param name="env">An <see cref="IWebHostEnvironment" /> representing the application's hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
            
        /// <summary>
        ///     Gets an <see cref="IConfiguration" /> representing the application's configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        ///     Gets an <see cref="IWebHostEnvironment" /> representing the application's hosting environment.
        /// </summary>
        private IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerDocument();
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
            
            // Add Swagger Middleware.
            app.UseOpenApi(config => { config.Path = Configuration["Swagger:OpenApi:Path"]; });
            app.UseSwaggerUi3(config =>
            {
                // Allow "Try it out" in development only.
                config.EnableTryItOut = Env.IsDevelopment();

                // Set Swagger website path and document route to match the controllers' base route.
                config.Path = Configuration["Swagger:SwaggerUi3:Path"];
                config.SwaggerRoutes.Add(new SwaggerUi3Route(
                    Configuration["Swagger:SwaggerUi3:SwaggerRoutes:Name"],
                    Configuration["Swagger:SwaggerUi3:SwaggerRoutes:Url"]
                ));
            });

        }
    }
}
