using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Configuration;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static LoggerExtensionBuilder AddLoggingExtensionForEFSqlServer<TDbContext>(this IServiceCollection services, UserProvider userProvider)
            where TDbContext : DbContext
        {
            return services.AddLoggingExtensionForEF<TDbContext>(userProvider, (services) =>
           {
               return new SqlEventLogConfiguration();
           });
        }
    }
}
