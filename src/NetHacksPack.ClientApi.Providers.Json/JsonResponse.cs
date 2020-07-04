using NetHacksPack.ClientApi.Abstractions;
using NetHacksPack.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetHacksPack.ClientApi.Providers
{
    public abstract class JsonResponse<T> : IResponse<T>
    {
        private readonly HttpResponseMessage httpResponseMessage;

        public JsonResponse(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        public virtual async Task<Result<T>> Response()
        {
            var data = await this.httpResponseMessage.Content.ReadAsStringAsync();

            if (this.httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // if any error on statu
                var errorList = new List<string>(1) { this.httpResponseMessage.ReasonPhrase };
                return new Result<T>(errorList);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
