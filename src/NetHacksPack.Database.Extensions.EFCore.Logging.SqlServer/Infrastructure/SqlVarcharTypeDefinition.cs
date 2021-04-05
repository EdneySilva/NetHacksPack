using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Infrastructure
{
    class SqlVarcharTypeDefinition : ColumnTypeCommandDefinition
    {
        private readonly int lenght;

        public SqlVarcharTypeDefinition(int lenght)
        {
            this.lenght = lenght;
        }
        public override string CreateCommand()
        {
            return $"VARCHAR({lenght})";
        }
    }


}
