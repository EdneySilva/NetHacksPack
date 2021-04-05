using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Events;
using NetHacksPack.Database.Extension.EFCore.Logging.Configuration;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Extension.EFCore.Logging.Providers;

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

        public static LoggerExtensionBuilder AddLoggingExtensionForEF<TDbContext>(this IServiceCollection services, string auditTableName, UserProvider userProvider, EventLogsConfigurationProvider eventLogsConfigurationProvider)
            where TDbContext : DbContext
        {
            var blackList = new List<IgnoredEntity>();
            services.AddScoped<IRequestHandler<ApplyConfigurationsToContextCommand, bool>, ContextConfigurationHandler>();
            services.AddSingleton<AuditTableNameProvider>(() => auditTableName);
            services.AddSingleton(userProvider);
            services.AddSingleton(blackList);
            services.AddSingleton(eventLogsConfigurationProvider);
            services.AddScoped<IDatabaseAuditProvider, DatabaseAuditProvider>();
            services.DisableAllLogOn<EventLog>();
            services.AddScoped<DbContextProvider>((serviceProvider) =>
            {
                return (service) => service.GetService<TDbContext>();
            });
            return new LoggerExtensionBuilder(services);
        }
    }
}
