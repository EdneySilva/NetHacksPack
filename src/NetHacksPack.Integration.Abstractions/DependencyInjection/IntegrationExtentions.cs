using NetHacksPack.Core;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IntegrationExtentions
    {
        public static IServiceCollection AddEventHandler<TEventHandler, TEventHandlerImplementation>(this IServiceCollection services)
            where TEventHandler : class, IEventHandler
            where TEventHandlerImplementation : class, TEventHandler
        {

            var iEventHandlerType = typeof(IEventHandler<>);
            var myIEventHandler = typeof(TEventHandler).GetInterfaces()
                .FirstOrDefault(
                    w =>
                        w.IsGenericType &&
                        w.GetGenericTypeDefinition() == iEventHandlerType);
            services.AddScoped<TEventHandler, TEventHandlerImplementation>();
            return services.AddScoped(myIEventHandler, typeof(TEventHandlerImplementation));
        }
        public static IServiceCollection AddEventHandler<TEventHandler, TEventHandlerImplementation>(this IServiceCollection services, ServiceLifetime serviceLifetime)
            where TEventHandler : class, IEventHandler
            where TEventHandlerImplementation : class, TEventHandler
        {

            var iEventHandlerType = typeof(IEventHandler<>);
            var myIEventHandler = typeof(TEventHandler).GetInterfaces()
                .FirstOrDefault(
                    w =>
                        w.IsGenericType &&
                        w.GetGenericTypeDefinition() == iEventHandlerType);
            services.Add(new ServiceDescriptor(typeof(TEventHandler), typeof(TEventHandlerImplementation), serviceLifetime));
            services.Add(new ServiceDescriptor(myIEventHandler, typeof(TEventHandlerImplementation), serviceLifetime));
            return services;
        }
    }
}