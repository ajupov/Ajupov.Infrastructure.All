using System;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.All.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}