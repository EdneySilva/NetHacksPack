namespace NetHacksPack.Database.Extension.EF.Infrastructure
{
    public abstract class CreateTableCommandDefinition : CommandDefinition
    {
        public abstract CreateTableCommandDefinition Schema(string name);

        public abstract CreateTableCommandDefinition Name(string name);

        public abstract CreateTableCommandDefinition Columns(ColumnCommandDefinition[] columns);
    }
}
