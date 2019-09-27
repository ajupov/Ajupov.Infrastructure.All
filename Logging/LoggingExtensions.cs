﻿using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.All.Logging
{
    public static class LoggingExtensions
    {
        private const string Template = "[{Timestamp:o} - {Level:u3}]: {Message:lj}{NewLine}{Exception}";

        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder webHostBuilder)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name;
            var applicationVersion = Assembly.GetCallingAssembly().GetName().Name;

            return webHostBuilder.ConfigureLogging(x =>
            {
                x.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: Template)
                    .WriteTo.File($"{applicationName}_{applicationVersion}_.txt", rollingInterval: RollingInterval.Day,
                        outputTemplate: Template, shared: true)
                    .CreateLogger();

                x.AddSerilog(Log.Logger);
            });
        }
    }
}