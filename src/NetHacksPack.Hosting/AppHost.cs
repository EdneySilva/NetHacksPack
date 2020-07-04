using Microsoft.Extensions.Hosting;
using NetHacksPack.Hosting.Configuration.Extensions;
using System.Threading.Tasks;
using System;

namespace NetHacksPack.Hosting
{
    public delegate void HostBuilderOptions(IHostBuilder hostBuilder);

    public static class AppHost
    {
        public static Task Run(string[] args, HostBuilderOptions hostBuilderOptions = null)
        {
            var hostBuilder = Host
                .CreateDefaultBuilder()
                .UseDefaultConfigurations(args);
            hostBuilderOptions?.Invoke(hostBuilder);
            return hostBuilder
                .Build()
                .RunAsync();
        }
    }
}
