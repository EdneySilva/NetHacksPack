using System.Net.Http;

namespace NetHacksPack.ClientApi.Abstractions
{
    public interface IRequest<T>
    {
        string Url { get; }
        HttpContent GetHttpContent();
        IResponse<T> CreateResponse(HttpResponseMessage httpResponseMessage);
    }
}
