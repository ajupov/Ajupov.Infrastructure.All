using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ajupov.Infrastructure.All.Hosting
{
    public static class HostingExtensions
    {
        public static IWebHostBuilder ConfigureHost(this IConfiguration configuration)
        {
            return new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseUrls(configuration.GetValue<string>("ApplicationHost"))
                .UseKestrel();
        }
    }
}