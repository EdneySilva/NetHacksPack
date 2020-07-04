using NetHacksPack.Hosting.Abstractions.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace NetHacksPack.Hosting.Configuration.Extensions
{
    internal static class HostConfiguration
    {
        public static IHostBuilder UseDefaultConfigurations(this IHostBuilder hostBuilder, string[] args)
        {
            hostBuilder.ConfigureHostConfiguration(configuration =>
            {
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddEnvironmentVariables(Prefixies.ENVIRONMENT_PREFIX);
                configuration.AddCommandLine(args);
                var environment = Environment.GetEnvironmentVariable(Prefixies.ENVIRONMENT_NAME);
                configuration
                    .AddJsonFile(Prefixies.APPSETTINGS_JSON, optional: false, reloadOnChange: true)
                    .AddJsonFile(string.Format(Prefixies.APPSETTINGS_ENV_JSON, environment), optional: true, reloadOnChange: true)
                    .AddJsonFile(Prefixies.APPSETTINGS_DATABASE_JSON, optional: true, reloadOnChange: true)
                    .AddJsonFile(Prefixies.APPSETTINGS_EVENT_BUS_JSON, optional: true, reloadOnChange: true)
                    .AddJsonFile(Prefixies.APPSETTINGS_LOG_JSON, optional: true, reloadOnChange: true)
                    .AddJsonFile(Prefixies.APPSETTINGS_CACHE_JSON, optional: true, reloadOnChange: true);
            }).ConfigureLogging(loggingBuilder =>
            {
                // lloggingBuilder.AddProvider()
            });
            return hostBuilder;
        }
    }
}
