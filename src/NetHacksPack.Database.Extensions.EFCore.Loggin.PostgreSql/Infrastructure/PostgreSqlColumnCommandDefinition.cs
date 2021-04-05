using NetHacksPack.Database.Extension.EF.Infrastructure;
using NetHacksPack.Database.Extension.EF.Infrastructure.Exceptions;
using System.Collections.Generic;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    class PostgreSqlColumnCommandDefinition : ColumnCommandDefinition
    {
        private Dictionary<string, string> definitions = new Dictionary<string, string>(3);

        private void SetData(string key, string value)
        {
            if (!definitions.ContainsKey(key))
                definitions.Add(key, string.Empty);
            definitions[key] = value;
        }

        public override ColumnCommandDefinition HasType(ColumnTypeCommandDefinition type)
        {
            this.SetData("TYPE", type.CreateCommand());
            return this;
        }

        public override ColumnCommandDefinition IsNotNull()
        {
            this.SetData("NULL", "IS NOT NULL");
            return this;
        }

        public override ColumnCommandDefinition IsNull()
        {
            this.SetData("NULL", "IS NULL");
            return this;
        }

        public override ColumnCommandDefinition Name(string name)
        {
            this.SetData("NAME", "\"" + name + "\"");
            return this;
        }

        public override ColumnCommandDefinition PrimaryKey()
        {
            SetData("PK", "NOT NULL PRIMARY KEY");
            return this;
        }

        public override string CreateCommand()
        {
            if (!this.definitions.ContainsKey("NAME"))
                throw new InvalidSintaxException("invalid column name, a column name must be informed to create a column");
            if(!this.definitions.ContainsKey("TYPE"))
                throw new InvalidSintaxException("invalid column type, a column type must be informed to create a column");
            return string.Join(" ", SafeGetValue("NAME"), SafeGetValue("TYPE"), SafeGetValue("NULL"), SafeGetValue("PK")).TrimEnd();
        }

        private string SafeGetValue(string key)
        {
            if (!this.definitions.ContainsKey(key))
                return string.Empty;
            return this.definitions[key] ?? string.Empty;
        }
    }
}
