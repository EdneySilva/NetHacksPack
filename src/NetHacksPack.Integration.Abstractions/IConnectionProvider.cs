namespace NetHacksPack.Integration.Abstractions
{
    public interface IConnectionProvider<T>
    {
        T GetConnection(string connectionString = null);
    }
}
