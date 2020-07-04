using System.Threading.Tasks;

namespace NetHacksPack.ClientApi.Abstractions
{
    public interface IApiContext
    {
        Task<IResponse<T>> Delete<T>(IRequest<T> request);
        Task<IResponse<T>> Get<T>(IRequest<T> request);
        Task<IResponse<T>> Post<T>(IRequest<T> request);
        Task<IResponse<T>> Put<T>(IRequest<T> request);
    }
}
