using Rx.Http.Exceptions;
using Rx.Http.Interceptors;
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

        public List<RxRequestInterceptor> RequestInterceptors { get; set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; set; }

        protected HttpClient http;

        protected object obj;

        private readonly RxHttpRequestOptions requestOptions;

        protected RxHttpRequest(
            HttpClient http, 
            string url = null, 
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null,
            Action<RxHttpRequestOptions> optionsCallback = null)
        {
            this.http = http;
            this.RequestInterceptors = requestInterceptors ?? new List<RxRequestInterceptor>();
            this.ResponseInterceptors = responseInterceptors ?? new List<RxResponseInterceptor>();
            this.Url = url;

            http.DefaultRequestHeaders.Clear();
            Headers = http.DefaultRequestHeaders;
            QueryStrings = new Dictionary<string, string>();

            this.requestOptions = new RxHttpRequestOptions(Headers, QueryStrings);
            optionsCallback?.Invoke(this.requestOptions);
        }

        protected abstract Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content);

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
            RequestMediaType = requestOptions.RequestMediaType;
            ResponseMediaType = requestOptions.ResponseMediaType;
            RequestInterceptors.AddRange(requestOptions.RequestInterceptors);
            ResponseInterceptors.AddRange(requestOptions.ResponseInterceptors);
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse : class
        {
            Setup();

            return SingleObservable.Create(async () =>
            {
                this.RequestInterceptors.ForEach(x => x.Intercept(this));
                var response = await CreateRequest().ConfigureAwait(false);
                this.ResponseInterceptors.ForEach(x => x.Intercept(response));
                
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
            Setup();

            return SingleObservable.Create(async () =>
            {
                this.RequestInterceptors.ForEach(x => x.Intercept(this));
                var response = await CreateRequest().ConfigureAwait(false);
                this.ResponseInterceptors.ForEach(x => x.Intercept(response));
                return await response.Content.ReadAsStringAsync();
            });
        }

        private async Task<HttpResponseMessage> CreateRequest()
        {
            var response = await ExecuteRequest(GetUri(), GetContent()).ConfigureAwait(false);
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