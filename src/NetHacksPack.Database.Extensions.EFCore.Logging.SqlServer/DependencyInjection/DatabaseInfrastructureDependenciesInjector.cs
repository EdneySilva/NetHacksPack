using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Extension.EF.Infrastructure;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using NetHacksPack.Database.Extension.EFCore.Logging.Providers;
using NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Configuration;
using NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static LoggerExtensionBuilder AddLoggingExtensionForEFSqlServer<TDbContext>(this IServiceCollection services, string tableName, UserProvider userProvider, string migrationTableName, string schemma = null)
            where TDbContext : DbContext
        {
            
            services.AddScoped<IDatabaseCommandGenerator, SqlServerCommandGenerator>();
            services.AddScoped<IHistoricalRepository, SqlHistoricalRepository>();
            services.AddSingleton<SchemmaNameProvider>(() => (schemma?.Length > 0 ? schemma : string.Empty));
            services.AddSingleton<MigrationTableNameProvider>(() => {
                return (schemma?.Length > 0 ? "[" + schemma + "]." : string.Empty) + "[" + migrationTableName + "]";
            });
            return services.AddLoggingExtensionForEF<TDbContext>(tableName, userProvider, (services) =>
            {
                return new SqlEventLogConfiguration(tableName);
            });
        }
    }
}
