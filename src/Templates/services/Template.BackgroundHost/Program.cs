using NetHacksPack.Hosting;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Template.BackgroundHost.DependecyInjection;

namespace Template.BackgroundHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AppHost.Run(args, (options) =>
            {
                options.ConfigureServices((context, services) =>
                {
                    services.AddDependencies(context.Configuration);
                });
            });
        }
    }
}
