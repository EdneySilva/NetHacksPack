namespace NetHacksPack.Core
{
    public interface IReplaceable<T>
    {
        T CopyAndReplace(T destination);
    }
}
