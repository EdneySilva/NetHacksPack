using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Extension.EF.Infrastructure;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Database.Extension.EFCore.Logging.Providers
{
    class DatabaseAuditProvider : IDatabaseAuditProvider
    {
        const string MIGRATION = "NetHacksPack.Database.Extension.EFCore.Logging.Providers";

        private readonly DbContext dbContext;
        private readonly IHistoricalRepository historicalRepository;
        private readonly IDatabaseCommandGenerator databaseCommandGenerator;
        private readonly SchemmaNameProvider schemmaNameProvider;
        private readonly AuditTableNameProvider auditTableName;

        public DatabaseAuditProvider(IServiceProvider serviceProvider, DbContextProvider dbContext, IHistoricalRepository historicalRepository, IDatabaseCommandGenerator databaseCommandGenerator, SchemmaNameProvider schemmaNameProvider, AuditTableNameProvider auditTableName)
        {
            this.dbContext = dbContext(serviceProvider);
            this.historicalRepository = historicalRepository;
            this.databaseCommandGenerator = databaseCommandGenerator;
            this.schemmaNameProvider = schemmaNameProvider;
            this.auditTableName = auditTableName;
        }

        public void Initialize()
        {
            var auditTableName = this.auditTableName();
            if (historicalRepository.HasEventLogTable(auditTableName))
                return;
            this.dbContext.Database.GetAppliedMigrations().ToList();
            var table = this.databaseCommandGenerator.GetCreateTableScript();
            table.Name(auditTableName);
            table.Schema(schemmaNameProvider());
            table.Columns(this.GetColumnStructure());
            var createLogTableScript = table.CreateCommand();
            this.dbContext.Database.ExecuteSqlRaw(createLogTableScript);
        }

        private ColumnCommandDefinition[] GetColumnStructure()
        {
            List<ColumnCommandDefinition> columns = new List<ColumnCommandDefinition>();
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("Id").HasType(this.databaseCommandGenerator.GetUniqueIdentifierColumn()).PrimaryKey());
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("CreatedAt").HasType(this.databaseCommandGenerator.GetDateTimeColumn()));
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("EntityType").HasType(this.databaseCommandGenerator.GetVarcharColumn(100)));
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("EventUser").HasType(this.databaseCommandGenerator.GetVarcharColumn(100)));
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("EventType").HasType(this.databaseCommandGenerator.GetVarcharColumn(50)));
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("DataKeysValues").HasType(this.databaseCommandGenerator.GetJsonColumn()));
            columns.Add(this.databaseCommandGenerator.CreateColumn().Name("OriginalValues").HasType(this.databaseCommandGenerator.GetJsonColumn()));
            return columns.ToArray();
        }
    }
}
