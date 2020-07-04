using NetHacksPack.Hosting.Abstractions.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NetHacksPack.Hosting.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetHacksPack.Hosting.Web.IIS.Configuration.Extensions
{
    internal static class HostConfiguration
    {
        public static IWebHostBuilder UseDefaultConfigurations(this IWebHostBuilder hostBuilder, string[] args)
        {
            hostBuilder.ConfigureAppConfiguration(configuration =>
            {
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddEnvironmentVariables(Prefixies.ENVIRONMENT_PREFIX);
                configuration.AddCommandLine(args);
                var environment = System.Environment.GetEnvironmentVariable($"ASP{Prefixies.ENVIRONMENT_NAME}");
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
            }).ConfigureHostPorts(args);
            return hostBuilder;
        }

        internal static IWebHostBuilder ConfigureHostPorts(this IWebHostBuilder webHostBuilder, string[] args)
        {
            if (args.Any(item => item.Split(' ').Any(param => param.StartsWith("--urls=http"))))
                return webHostBuilder;
            var httpPort = ($"ASP{Prefixies.ENVIRONMENT_PREFIX}HOST_PORT").GetString(null);
            var httpsPort = ($"ASP{Prefixies.ENVIRONMENT_PREFIX}HOST_HTTPS_PORT").GetString(null);
            var ports = new List<string>();
            if (!string.IsNullOrEmpty(httpPort))
                ports.Add("http://localhost:" + httpPort);
            if (!string.IsNullOrEmpty(httpsPort))
                ports.Add("https://localhost:" + httpsPort);
            if (ports.Any())
                webHostBuilder.UseUrls(ports.ToArray());
            return webHostBuilder;
        }
    }
}
