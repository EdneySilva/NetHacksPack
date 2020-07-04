using NetHacksPack.Hosting.Web.IIS.Configuration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace NetHacksPack.Hosting.Web.IIS
{
    public delegate void IISHostBuilderOptions(IWebHostBuilder hostBuilder);
    public static class IISHost
    {
        public static Task Run(string[] args, IISHostBuilderOptions hostBuilderOptions = null)
        {
            var hostBuilder = WebHost
                .CreateDefaultBuilder()
                .UseDefaultConfigurations(args);
            hostBuilderOptions?.Invoke(hostBuilder);
            return hostBuilder
                .Build()
                .RunAsync();
        }
    }
}
