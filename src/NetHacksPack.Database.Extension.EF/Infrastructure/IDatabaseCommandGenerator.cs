using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EF.Infrastructure
{
    public interface IDatabaseCommandGenerator
    {
        CreateTableCommandDefinition GetCreateTableScript();

        string GetInsertScript();

        ColumnTypeCommandDefinition GetVarcharColumn(int lenght);

        ColumnTypeCommandDefinition GetDateTimeColumn();

        ColumnTypeCommandDefinition GetJsonColumn();

        ColumnCommandDefinition CreateColumn();

        ColumnTypeCommandDefinition GetUniqueIdentifierColumn();
    }
}
