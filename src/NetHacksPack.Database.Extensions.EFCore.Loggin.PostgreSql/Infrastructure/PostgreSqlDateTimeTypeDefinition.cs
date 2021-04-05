using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    class PostgreSqlDateTimeTypeDefinition : ColumnTypeCommandDefinition
    {
        public override string CreateCommand()
        {
            return "TIMESTAMP";
        }
    }
}
