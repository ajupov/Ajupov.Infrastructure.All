using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Ajupov.Infrastructure.All.Hosting
{
    public static class HostingExtensions
    {
        public static IWebHostBuilder ConfigureHosting<TStartup>(this IConfiguration configuration)
            where TStartup : class
        {
            return new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseUrls(configuration.GetValue<string>("ApplicationHost"))
                .UseStartup<TStartup>()
                .UseKestrel()
                .UseSerilog();
        }

        public static IWebHostBuilder ConfigureWebRoot(this IWebHostBuilder builder)
        {
            return builder
                .UseWebRoot(Directory.GetCurrentDirectory());
        }
    }
}
