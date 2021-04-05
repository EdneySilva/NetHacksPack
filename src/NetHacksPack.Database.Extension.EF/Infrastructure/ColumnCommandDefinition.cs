namespace NetHacksPack.Database.Extension.EF.Infrastructure
{
    public abstract class ColumnCommandDefinition : CommandDefinition
    {
        public abstract ColumnCommandDefinition Name(string name);

        public abstract ColumnCommandDefinition IsNull();

        public abstract ColumnCommandDefinition IsNotNull();

        public abstract ColumnCommandDefinition HasType(ColumnTypeCommandDefinition type);

        public abstract ColumnCommandDefinition PrimaryKey();
    }
}
