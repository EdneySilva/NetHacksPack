using NetHacksPack.Database.Extension.EF.Infrastructure;
using System;
using System.Text;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    class SqlServerCommandGenerator : IDatabaseCommandGenerator
    {
        public ColumnCommandDefinition CreateColumn()
        {
            return new SqlColumnCommandDefinition();
        }

        public ColumnTypeCommandDefinition GetUniqueIdentifierColumn()
        {
            return new SqlUniquekeyTypeDefinition();
        }

        public CreateTableCommandDefinition GetCreateTableScript()
        {
            return new SqlCreateTableCommandDefinition();
        }

        public ColumnTypeCommandDefinition GetDateTimeColumn()
        {
            return new SqlDateTimeTypeDefinition();
        }

        public string GetInsertScript()
        {
            return "INSERT INTO {0} VALUES ({0})";
        }

        public ColumnTypeCommandDefinition GetJsonColumn()
        {
            return new SqlJsonTypeDefinition();
        }

        public ColumnTypeCommandDefinition GetVarcharColumn(int lenght)
        {
            return new SqlVarcharTypeDefinition(lenght);
        }
    }
}
