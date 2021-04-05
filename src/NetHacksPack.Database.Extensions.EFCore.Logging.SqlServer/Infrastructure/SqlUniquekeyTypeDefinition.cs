using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    class SqlUniquekeyTypeDefinition : ColumnTypeCommandDefinition
    {
        public override string CreateCommand()
        {
            return "UNIQUEIDENTIFIER";
        }
    }


}
