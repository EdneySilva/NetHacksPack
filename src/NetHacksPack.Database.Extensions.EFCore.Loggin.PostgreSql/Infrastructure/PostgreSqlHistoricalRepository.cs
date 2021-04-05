using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Extension.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    public delegate string MigrationTableNameProvider();

    class PostgreSqlHistoricalRepository : IHistoricalRepository
    {
        private readonly DbContext dbContext;
        private readonly MigrationTableNameProvider migrationTableNameProvider;

        public PostgreSqlHistoricalRepository(DbContext dbContext, MigrationTableNameProvider migrationTableNameProvider)
        {
            this.dbContext = dbContext;
            this.migrationTableNameProvider = migrationTableNameProvider;
        }

        public string GetInsertScript(string key, string value)
        {
            return "INSERT INTO " + migrationTableNameProvider() + $" VALUES ('{key}', '{value}')";
        }

        public bool HasEventLogTable(string migrationId)
        {
            return this.dbContext.Database.GetAppliedMigrations().Any(a => a == migrationId);
        }
    }
}
