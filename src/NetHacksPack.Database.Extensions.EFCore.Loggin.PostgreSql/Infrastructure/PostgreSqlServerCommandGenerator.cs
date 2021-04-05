using NetHacksPack.Database.Extension.EF.Infrastructure;
using System;
using System.Text;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    class PostgreSqlServerCommandGenerator : IDatabaseCommandGenerator
    {
        public ColumnCommandDefinition CreateColumn()
        {
            return new PostgreSqlColumnCommandDefinition();
        }

        public ColumnTypeCommandDefinition GetUniqueIdentifierColumn()
        {
            return new PostgreSqlUniquekeyTypeDefinition();
        }

        public CreateTableCommandDefinition GetCreateTableScript()
        {
            return new PostgreSqlCreateTableCommandDefinition();
        }

        public ColumnTypeCommandDefinition GetDateTimeColumn()
        {
            return new PostgreSqlDateTimeTypeDefinition();
        }

        public string GetInsertScript()
        {
            return "INSERT INTO {0} VALUES ({0})";
        }

        public ColumnTypeCommandDefinition GetJsonColumn()
        {
            return new PostgreSqlJsonTypeDefinition();
        }

        public ColumnTypeCommandDefinition GetVarcharColumn(int lenght)
        {
            return new PostgreSqlVarcharTypeDefinition(lenght);
        }
    }
}
