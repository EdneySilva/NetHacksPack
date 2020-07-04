using System.Threading.Tasks;
using NetHacksPack.Core;

namespace NetHacksPack.ClientApi.Abstractions
{
    public interface IResponse<T>
    {
        Task<Result<T>> Response();
    }
}
