using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Configuration;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static LoggerExtensionBuilder AddLoggingExtensionForEFSqlServer(this IServiceCollection services, UserProvider userProvider)
        {
            return services.AddLoggingExtensionForEF(userProvider, (services) =>
            {
                return new SqlEventLogConfiguration();
            });
        }
    }
}
