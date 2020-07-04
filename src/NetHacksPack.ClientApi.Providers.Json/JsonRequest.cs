using NetHacksPack.ClientApi.Abstractions;
using NetHacksPack.ClientApi.Constants;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace NetHacksPack.ClientApi.Providers.Json
{
    public abstract class JsonRequest<T> : IRequest<T>
    {
        public virtual string Url { get; protected set; }

        public abstract IResponse<T> CreateResponse(HttpResponseMessage httpResponseMessage);

        public HttpContent GetHttpContent()
        {
            return new StringContent(JsonConvert.SerializeObject(this.GetData()), Encoding.UTF8, ApplicationsType.JSON);
        }

        protected abstract T GetData();
    }
}
