using Rx.Http.Exceptions;
using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest
    {
        private string url;
        public string Url { get => GetUri(); set => url = value; }
        public Dictionary<string, string> QueryStrings { get; set; }
        public HttpHeaders Headers { get; set; }

        public IHttpMediaType RequestMediaType { get; set; }
        public IHttpMediaType ResponseMediaType { get; set; }

        protected Action<RxHttpRequestOptions> optionsCallback { get; set; }

        protected HttpClient http;

        protected object obj;

        public RxHttpRequest(HttpClient http)
        {
            http.DefaultRequestHeaders.Clear();
            Headers = http.DefaultRequestHeaders;
            QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        protected abstract Task<HttpResponseMessage> DoRequest(string url, HttpContent content);

        public string GetUri()
        {
            var builder = new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var entry in QueryStrings)
            {
                query[entry.Key] = entry.Value;
            }

            builder.Query = query.ToString();

            var urlFull = url + builder.Query;
            return urlFull;
        }

        private void Setup()
        {
            var options = new RxHttpRequestOptions(Headers, QueryStrings);
            optionsCallback?.Invoke(options);

            RequestMediaType = RequestMediaType ?? options.RequestMediaType;
            ResponseMediaType = ResponseMediaType ?? options.ResponseMediaType;
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse : class
        {
            Setup();

            return SingleObservable.Create(async () =>
            {
                var response = await GetResponse();

                if (ResponseMediaType == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    ResponseMediaType = MediaTypesMap.Get(mimeType);
                }

                var responseObject = ResponseMediaType.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
                return responseObject;
            });
        }

        internal IObservable<string> Request()
        {
            return SingleObservable.Create(async () => 
            {
                var response = await GetResponse();
                return await response.Content.ReadAsStringAsync();
            });
        }

        private async Task<HttpResponseMessage> GetResponse()
        {
            var response = await DoRequest(GetUri(), GetContent());
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                throw new RxHttpRequestException(response, exception);
            }
            return response;
        }

        private HttpContent GetContent()
        {
            HttpContent httpContent = null;

            if (obj != null)
            {
                if (obj is HttpContent)
                {
                    httpContent = obj as HttpContent;
                }
                else
                {
                    if (RequestMediaType == null)
                    {
                        RequestMediaType = MediaTypesMap.Get(MediaType.Application.Json);
                    }

                    httpContent = RequestMediaType.Serialize(obj);
                }
            }

            return httpContent;
        }
    }
}