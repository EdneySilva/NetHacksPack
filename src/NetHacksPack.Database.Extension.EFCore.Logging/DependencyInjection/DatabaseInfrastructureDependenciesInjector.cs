using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Events;
using NetHacksPack.Database.Extension.EFCore.Logging.Configuration;
using System.Collections.Generic;
using MediatR;

namespace NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static IServiceCollection DisableAllLogOn<TEntidade>(this IServiceCollection services)
        {
            services.AddSingleton(
                new IgnoredEntity(typeof(TEntidade),
                new[]
                {
                    typeof(DataAddedEvent<>).Name,
                    typeof(DataUpdatedEvent<>).Name,
                    typeof(DataDeletedEvent<>).Name,
                }));
            return services;
        }

        public static LoggerExtensionBuilder AddLoggingExtensionForEF(this IServiceCollection services, UserProvider userProvider, EventLogsConfigurationProvider eventLogsConfigurationProvider)
        {
            var blackList = new List<IgnoredEntity>();
            services.AddScoped<IRequestHandler<ApplyConfigurationsToContextCommand, bool>, ContextConfigurationHandler>();
            services.AddSingleton(userProvider);
            services.AddSingleton(eventLogsConfigurationProvider);
            services.AddSingleton(blackList); 
            services.DisableAllLogOn<EventLog>();

            return new LoggerExtensionBuilder(services);
        }
    }
}
