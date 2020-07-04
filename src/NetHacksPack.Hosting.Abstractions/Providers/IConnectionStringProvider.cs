namespace NetHacksPack.Hosting.Abstractions.Providers
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string connectionKey);

        TConnector GetConnectorOptions<TConnector>(string connectionKey)
            where TConnector : new();
    }
}
