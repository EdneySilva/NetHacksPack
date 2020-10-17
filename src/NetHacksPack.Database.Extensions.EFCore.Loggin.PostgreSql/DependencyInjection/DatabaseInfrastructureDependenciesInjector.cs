using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Configuration;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static LoggerExtensionBuilder AddLoggingExtensionForEFPostgreSql(this IServiceCollection services, UserProvider userProvider)
        {
            return services.AddLoggingExtensionForEF(userProvider, (services) =>
            {
                return new PostgreSqlEventLogConfiguration();
            });
        }
    }
}
