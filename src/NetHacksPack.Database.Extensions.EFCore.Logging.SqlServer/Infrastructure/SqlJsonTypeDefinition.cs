using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    class SqlJsonTypeDefinition : ColumnTypeCommandDefinition
    {
        public override string CreateCommand()
        {
            return "NVARCHAR(MAX)";
        }
    }
}
