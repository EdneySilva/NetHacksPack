using NetHacksPack.ClientApi.Abstractions;
using NetHacksPack.ClientApi.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetHacksPack.ClientApi
{
    class ApiContext : IApiContext
    {
        private readonly ApiConnection apiConnection;
        public ApiContext(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        private string BuildUrl(string url)
        {
            return (this.apiConnection.Url.EndsWith("/") ? this.apiConnection.Url : (this.apiConnection.Url + "/"))
                + (
                    url.StartsWith("/") ?
                    url.Substring(0) :
                    url
                );
        }

        public async Task<IResponse<T>> Delete<T>(IRequest<T> request)
        {
            var httpContent = request.GetHashCode();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(new Uri(this.BuildUrl(request.Url)));
                return request.CreateResponse(response);
            }
        }

        public async Task<IResponse<T>> Get<T>(IRequest<T> request)
        {
            var httpContent = request.GetHashCode();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(this.BuildUrl(request.Url)));
                return request.CreateResponse(response);
            }
        }

        public async Task<IResponse<T>> Post<T>(IRequest<T> request)
        {
            var httpContent = request.GetHashCode();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(new Uri(this.BuildUrl(request.Url)), request.GetHttpContent());
                return request.CreateResponse(response);
            }
        }

        public Task<IResponse<T>> Put<T>(IRequest<T> request)
        {
            throw new NotImplementedException();
        }
    }
}
