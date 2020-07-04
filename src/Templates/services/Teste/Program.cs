using NetHacksPack.Hosting;
using System;
using System.Threading.Tasks;
using Template.BackgroundHost.DependecyInjection;

namespace Teste
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AppHost.Run(args, (options) =>
            {
                options.ConfigureServices((context, services) =>
                {
                    //var handler = new NetHacksPack.Integration.RabbitMQ.Handlers.RetryMessageHandler();
                    //Console.WriteLine(handler);
                    Console.WriteLine("Hello World!");
                    services.AddDependencies(context.Configuration);
                });
            });
        }
    }
}
