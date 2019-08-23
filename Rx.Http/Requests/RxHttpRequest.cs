using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Rx.Http.MediaTypes;
using Rx.Http.Serializers.Interfaces;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest
    {
        public string Url {get;set;}
        public Dictionary<string, string> QueryStrings{get;set;}
        public HttpHeaders Headers {get;set;}
        public IDeserializer Deserializer {get;set;}
        public ISerializer Serializer {get;set;}

        internal HttpClient http;

        internal object obj;

        private ILogger logger;

        public RxHttpRequest(HttpClient http)
        {
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        public RxHttpRequest(HttpClient http, ILogger logger)
        {
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
            this.logger = logger;
        }

        public abstract string MethodName { get; internal set; }
        internal abstract Task<HttpResponseMessage> HttpMethod(string url, HttpContent content);


        public Uri GetUri()
        {
            UriBuilder builder = null;
            if(http?.BaseAddress != null)
            {
                builder = new UriBuilder(http.BaseAddress.AbsoluteUri + Url);
            }
            else
            {
                builder = new UriBuilder(Url);
            }

            var query = HttpUtility.ParseQueryString(builder.Query);
            
            foreach (var entry in QueryStrings)
            {
                query[entry.Key] = entry.Value;    
            }

            builder.Query = query.ToString();
            return builder.Uri;
        }

        internal void Setup(Action<RxHttpRequestOptions> opts)
        {
            var options = new RxHttpRequestOptions(http.DefaultRequestHeaders);
            opts?.Invoke(options);

            this.Headers = options.Headers;
            this.QueryStrings = options.QueryStrings;
            this.Serializer = options.Serializer;
            this.Deserializer = options.Deserializer;
        }

        internal IObservable<string> Request()
        {
            return SingleObservable.Create(async () =>
            {
                var uri = GetUri();
                logger?.LogInformation($"{MethodName.ToUpper()}: {uri.AbsoluteUri}");

                var response = await http.GetAsync(uri);
                logger?.LogInformation($"Server response: {response.ReasonPhrase}({response.StatusCode}) => {response.Content.Headers.ContentType}");

                if (!response.IsSuccessStatusCode)
                {
                    logger?.LogError($"Server response body:\n{ await response.Content.ReadAsStringAsync() }");
                }

                var deserialized = await response.Content.ReadAsStringAsync();

                return deserialized;
            });
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse: class
        {
            return SingleObservable.Create(async () =>
            {
                var response = await http.GetAsync(GetUri());

                if (Deserializer == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    var mediaType = MediaTypesMap.GetMediaType(mimeType);
                    Deserializer = mediaType.BodySerializer;
                }

                return Deserializer.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
            });
        }
    }
}