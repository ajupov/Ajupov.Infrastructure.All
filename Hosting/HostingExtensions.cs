using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ajupov.Infrastructure.All.Hosting
{
    public static class HostingExtensions
    {
        public static IWebHostBuilder ConfigureHost(this IConfiguration configuration)
        {
            return WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseConfiguration(configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(configuration.GetValue<string>("ApplicationHost"));
        }
    }
}