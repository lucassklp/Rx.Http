using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Web;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest
    {
        private string url;
        public string Url { get => GetUri(); set => this.url = value; }
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
            this.Headers = http.DefaultRequestHeaders;
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        protected abstract string MethodName { get; set; }
        protected abstract Task<HttpResponseMessage> DoRequest(string url, HttpContent content);

        public string GetUri()
        {

/* Unmerged change from project 'Rx.Http (net45)'
Before:
            UriBuilder builder = new UriBuilder(url);
            
            var query = HttpUtility.ParseQueryString(builder.Query);
After:
            UriBuilder builder = new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(builder.Query);
*/
            UriBuilder builder = new UriBuilder(url);

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
            var options = new RxHttpRequestOptions(this.Headers, this.QueryStrings);
            optionsCallback?.Invoke(options);

            this.RequestMediaType = this.RequestMediaType ?? options.RequestMediaType;
            this.ResponseMediaType = this.ResponseMediaType ?? options.ResponseMediaType;
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse : class
        {
            Setup();

            async Task<TResponse> Create()
            {
                var response = await DoRequest(GetUri(), GetContent());
                response.EnsureSuccessStatusCode();

                if (ResponseMediaType == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    ResponseMediaType = MediaTypesMap.GetMediaType(mimeType);
                }

                var responseObject = ResponseMediaType.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
                return responseObject;
            }

            return Create().ToObservable();
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
                        RequestMediaType = MediaTypesMap.GetMediaType(MediaType.Application.Json);
                    }
                    httpContent = RequestMediaType.Serialize(this.obj);

                }
            }

            return httpContent;
        }

        internal IObservable<string> Request()
        {
            async Task<string> ReqAsync() => await (await DoRequest(GetUri(), GetContent())).Content.ReadAsStringAsync();
            return ReqAsync().ToObservable();
        }
    }
}