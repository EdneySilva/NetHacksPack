using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Extension.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetHacksPack.Database.Extension.EFCore.Logging.Infrastructure
{
    class HistoricalRepository : IHistoricalRepository
    {
        private readonly DbContext dbContext;
        private readonly IDatabaseCommandGenerator databaseCommandGenerator;

        public HistoricalRepository(DbContext dbContext, IDatabaseCommandGenerator databaseCommandGenerator)
        {
            this.dbContext = dbContext;
            this.databaseCommandGenerator = databaseCommandGenerator;
        }

        public string GetInsertScript(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool HasEventLogTable(string migrationId)
        {
            return this.dbContext.Database.GetAppliedMigrations().Any(a => a == migrationId);
        }
    }
}
