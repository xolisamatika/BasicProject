using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BasicProject.Api
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        ///     Provides the application's main entry point.
        /// </summary>
        /// <param name="args">A <see cref="string" />[] representing the application's arguments.</param>
        public static void Main(string[] args)
        {
            /*
             * CRITICAL: APIs must use the invariant culture.
             */
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Builds the application's host.
        /// </summary>
        /// <param name="args">A <see cref="string" />[] representing the application's arguments.</param>
        /// <returns>An <see cref="IHostBuilder" /> representing the host builder.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
