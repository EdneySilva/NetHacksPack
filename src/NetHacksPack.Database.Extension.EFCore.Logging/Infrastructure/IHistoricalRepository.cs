namespace NetHacksPack.Database.Extension.EFCore.Logging.Infrastructure
{
    interface IHistoricalRepository2
    {
        bool HasMisgration(string migrationId);

        string GetInsertScript(string key, string value);
    }
}