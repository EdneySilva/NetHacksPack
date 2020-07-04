using NetHacksPack.ClientApi;
using NetHacksPack.ClientApi.Abstractions;
using NetHacksPack.ClientApi.Configuration;
using NetHacksPack.Hosting.Abstractions.Providers;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ClientApiExtension
    {
        public static IServiceCollection AddApiContext(this IServiceCollection services, Func<IConnectionStringProvider, ApiConnection> connectionStringProvider)
        {
            return services
                .AddScoped<IApiContext>((serviceProvider) =>
                {
                    var connectionBuilder = serviceProvider.GetService<IConnectionStringProvider>();
                    return new ApiContext(connectionStringProvider(connectionBuilder));
                });
        }
    }
}
