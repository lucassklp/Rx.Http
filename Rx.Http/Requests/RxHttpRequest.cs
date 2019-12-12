using Microsoft.Net.Http.Headers;
using Rx.Http.Exceptions;
using Rx.Http.Interceptors;
using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest : RxHttpRequestOptions
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

        private readonly RxHttpLogging logger;

        protected RxHttpRequest(
            HttpClient http, 
            string url = null, 
            List<RxRequestInterceptor> requestInterceptors = null,
            List<RxResponseInterceptor> responseInterceptors = null,
            Action<RxHttpRequestOptions> optionsCallback = null, 
            RxHttpLogging logger = null)
        {
            this.http = http;
            this.RequestInterceptors = requestInterceptors ?? new List<RxRequestInterceptor>();
            this.ResponseInterceptors = responseInterceptors ?? new List<RxResponseInterceptor>();
            this.Url = url;
            this.logger = logger;

            http.DefaultRequestHeaders.Clear();
            Headers = http.DefaultRequestHeaders;
            QueryStrings = new Dictionary<string, string>();

            optionsCallback?.Invoke(this);
        }

        protected abstract Task<HttpResponseMessage> ExecuteRequest(string url, HttpContent content);

        public string GetUri()
        {
            var builder = new UriBuilder(http.BaseAddress + url);

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var entry in QueryStrings)
            {
                query[entry.Key] = entry.Value;
            }

            builder.Query = query.ToString();

            var urlFull = url + builder.Query;
            return urlFull;
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse : class
        {
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
            var content = GetContent();
            logger?.OnSend(content);
            var response = await ExecuteRequest(Url, content).ConfigureAwait(false);
            try
            {
                logger?.OnReceive(response);
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


        public override RxHttpRequestOptions AddHeader(string key, string value)
        {
            Headers.Add(key, value);
            return this;
        }

        public override RxHttpRequestOptions AddHeader(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            pairs.ToList().ForEach(x => AddHeader(x.Key, x.Value));
            return this;
        }

        public override RxHttpRequestOptions AddHeader(object obj)
        {
            AddHeader(GetKeysByObject(obj));
            return this;
        }

        public override RxHttpRequestOptions AddQueryString(string key, string value)
        {
            this.QueryStrings.Add(key, value);
            return this;

        }

        public override RxHttpRequestOptions AddQueryString(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            pairs.ToList().ForEach(x => AddQueryString(x.Key, x.Value));
            return this;

        }

        public override RxHttpRequestOptions AddQueryString(object obj)
        {
            AddQueryString(GetKeysByObject(obj));
            return this;
        }

        public override RxHttpRequestOptions AddRequestInteceptor(RxRequestInterceptor interceptor)
        {
            this.RequestInterceptors.Add(interceptor);
            return this;
        }

        public override RxHttpRequestOptions AddResponseInterceptor(RxResponseInterceptor interceptor)
        {
            this.ResponseInterceptors.Add(interceptor);
            return this;
        }

        public override RxHttpRequestOptions SetRequestMediaType(IHttpMediaType mediaType)
        {
            this.RequestMediaType = mediaType;
            return this;
        }

        public override RxHttpRequestOptions SetResponseMediaType(IHttpMediaType mediaType)
        {
            this.ResponseMediaType = mediaType;
            return this;
        }

        public override RxHttpRequestOptions UseBasicAuthorization(string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
            return this;
        }

        public override RxHttpRequestOptions UseBearerAuthorization(string token)
        {
            AddHeader(HeaderNames.Authorization, $"Bearer {token}");
            return this;

        }

    }
}