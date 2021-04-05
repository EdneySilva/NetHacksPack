using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Extension.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    public delegate string MigrationTableNameProvider();

    class SqlHistoricalRepository : IHistoricalRepository
    {
        private readonly DbContext dbContext;
        private readonly MigrationTableNameProvider migrationTableNameProvider;

        public SqlHistoricalRepository(DbContext dbContext, MigrationTableNameProvider migrationTableNameProvider)
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
            var cnn = this.dbContext.Database.GetDbConnection();
            cnn.Open();
            var cmd = cnn.CreateCommand();
            cmd.CommandText = $"if(object_id('{migrationId}') is not null) select 1";
            var reader = cmd.ExecuteReader();
            var result = reader.HasRows;
            reader.Close();
            cnn.Close();
            return result;
        }
    }
}
