namespace NetHacksPack.Database.Extension.EF.Infrastructure
{
    public abstract class CommandDefinition
    {
        public static implicit operator string(CommandDefinition commandDefinition)
        {
            return commandDefinition.CreateCommand();
        }

        public abstract string CreateCommand();
    }
}
