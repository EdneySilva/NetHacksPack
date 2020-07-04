namespace NetHacksPack.Integration.Abstractions.IO
{
    public interface IMessageReader
    {
        string ReadHeader(string headerName);
        TResult Read<TResult>();
    }
}
