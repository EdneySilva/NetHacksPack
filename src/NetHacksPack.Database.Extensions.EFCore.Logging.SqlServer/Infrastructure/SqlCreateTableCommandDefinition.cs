using NetHacksPack.Database.Extension.EF.Infrastructure;
using NetHacksPack.Database.Extension.EF.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    class SqlCreateTableCommandDefinition : CreateTableCommandDefinition
    {
        private Dictionary<string, string> definitions = new Dictionary<string, string>(3);
        private ColumnCommandDefinition[] columns = null;

        private void SetData(string key, string value)
        {
            if (!definitions.ContainsKey(key))
                definitions.Add(key, string.Empty);
            definitions[key] = value;
        }

        public override string CreateCommand()
        {
            if (!this.definitions.ContainsKey("NAME"))
                throw new InvalidSintaxException("invalid table name, a table name must be informed to create a table");
            if(columns == null || columns.Length == 0)
                throw new InvalidSintaxException("invalid columns definitions, a set of columns must be informed to create a table");

            return "CREATE TABLE " + (SafeGetValue("SCHEMA").Any() ? "[" + SafeGetValue("SCHEMA") + "]." : string.Empty) + "[" + SafeGetValue("NAME") + "](\n" +
                string.Join(",\n", columns.Select(s => s.CreateCommand()).ToArray()) +
            ")";
        }

        public override CreateTableCommandDefinition Name(string name)
        {
            SetData("NAME", name);
            return this;
        }

        public override CreateTableCommandDefinition Schema(string name)
        {
            SetData("SCHEMA", name);
            return this;
        }

        private string SafeGetValue(string key)
        {
            if (!this.definitions.ContainsKey(key))
                return string.Empty;
            return this.definitions[key] ?? string.Empty;
        }

        public override CreateTableCommandDefinition Columns(ColumnCommandDefinition[] columns)
        {
            this.columns = columns;
            return this;
        }
    }


}
