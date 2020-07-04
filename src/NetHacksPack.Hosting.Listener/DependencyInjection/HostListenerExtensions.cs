using NetHacksPack.Hosting.Listener;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HostListenerExtensions
    {
        public static IServiceCollection AddEventBusListenerHostedService<THostedService>(this IServiceCollection services)
            where THostedService : EventBusListenerBackgroundService
        {
            services.AddHostedService<THostedService>();
            return services;
        }
    }
}
