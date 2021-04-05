using NetHacksPack.Database.Extension.EF.Infrastructure;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Infrastructure
{
    class PostgreSqlVarcharTypeDefinition : ColumnTypeCommandDefinition
    {
        private readonly int lenght;

        public PostgreSqlVarcharTypeDefinition(int lenght)
        {
            this.lenght = lenght;
        }
        public override string CreateCommand()
        {
            return $"VARCHAR({lenght})";
        }
    }
}
