using System.Threading.Tasks;
using ECorp.Hosting.Web.IIS;
using Microsoft.AspNetCore.Hosting;

namespace ECorp.Realtime.SignalR
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await IISHost.Run(args, (webHostBuilder) =>
            {
                webHostBuilder.UseStartup<Startup>();
            });
        }
    }
}
