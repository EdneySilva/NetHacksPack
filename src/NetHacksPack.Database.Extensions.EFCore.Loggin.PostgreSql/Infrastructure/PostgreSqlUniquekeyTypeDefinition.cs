using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    class PostgreSqlUniquekeyTypeDefinition : ColumnTypeCommandDefinition
    {
        public override string CreateCommand()
        {
            return "UUID";
        }
    }
}
