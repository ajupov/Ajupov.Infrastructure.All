using System;
using Microsoft.Extensions.Configuration;

namespace Ajupov.Infrastructure.All.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}