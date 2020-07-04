using NetHacksPack.Hosting.Abstractions.Providers;
using NetHacksPack.Hosting.Connections;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConnectorExtensions
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
        }
    }
}
