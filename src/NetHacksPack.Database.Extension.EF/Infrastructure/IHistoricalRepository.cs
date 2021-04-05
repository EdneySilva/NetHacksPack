namespace NetHacksPack.Database.Extension.EF.Infrastructure
{
    public interface IHistoricalRepository
    {
        bool HasEventLogTable(string migrationId);

        string GetInsertScript(string key, string value);
    }
}
