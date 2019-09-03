using Microsoft.Extensions.Logging;
using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Rx.Http.Requests
{
    public abstract class RxHttpRequest
    {
        private string url;
        public string Url { get => GetUri().AbsoluteUri; set => this.url = value; }
        public Dictionary<string, string> QueryStrings { get; set; }
        public HttpHeaders Headers { get; set; }

        public IHttpMediaType RequestMediaType { get; set; }
        public IHttpMediaType ResponseMediaType { get; set; }

        internal Action<RxHttpRequestOptions> optionsCallback { get; set; }

        internal HttpClient http;

        internal object obj;

        private ILogger logger;

        public RxHttpRequest(HttpClient http)
        {
            http.DefaultRequestHeaders.Clear();
            this.Headers = http.DefaultRequestHeaders;
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
        }

        public RxHttpRequest(HttpClient http, ILogger logger)
        {
            http.DefaultRequestHeaders.Clear();
            this.Headers = http.DefaultRequestHeaders;
            this.QueryStrings = new Dictionary<string, string>();
            this.http = http;
            this.logger = logger;
        }

        internal abstract string MethodName { get; set; }
        internal abstract Task<HttpResponseMessage> DoRequest(string url, HttpContent content);

        public Uri GetUri()
        {
            UriBuilder builder = null;
            if (http?.BaseAddress != null)
            {
                builder = new UriBuilder(http.BaseAddress.AbsoluteUri + url);
            }
            else
            {
                builder = new UriBuilder(url);
            }

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var entry in QueryStrings)
            {
                query[entry.Key] = entry.Value;
            }

            builder.Query = query.ToString();
            return builder.Uri;
        }

        private void Setup()
        {
            var options = new RxHttpRequestOptions(this.Headers, this.QueryStrings);
            optionsCallback?.Invoke(options);
       
            this.RequestMediaType = this.RequestMediaType ?? options.RequestMediaType;
            this.ResponseMediaType = this.ResponseMediaType ?? options.ResponseMediaType;
        }

        internal IObservable<string> Request()
        {
            logger?.LogTrace("Applying the options");
            Setup();
            logger?.LogTrace("The options were applied, executing the request");

            return SingleObservable.Create(async () =>
            {
                var uri = GetUri();
                logger?.LogInformation($"{MethodName.ToUpper()} {uri.AbsoluteUri}");

                HttpContent httpContent = null;

                if(obj != null)
                {
                    if (obj is HttpContent)
                    {
                        httpContent = obj as HttpContent;
                    }
                    else
                    {
                        if (RequestMediaType == null)
                        {
                            logger?.LogTrace("RequestMediaType is null, using the default (application/json)");
                            RequestMediaType = MediaTypesMap.GetMediaType(MediaType.Application.Json);
                        }

                        Stopwatch serializeWatch = new Stopwatch();
                        serializeWatch.Start();
                        httpContent = RequestMediaType.Serialize(this.obj);
                        serializeWatch.Stop();
                        logger?.LogInformation($"Serialization completed successfully in {serializeWatch.ElapsedMilliseconds}ms");
                        logger?.LogTrace($"Request content: {await httpContent.ReadAsStringAsync()}");
                    }
                }

                Stopwatch requestWatch = new Stopwatch();
                requestWatch.Start();
                var response = await DoRequest(uri.AbsoluteUri, httpContent);
                requestWatch.Stop();

                logger?.LogInformation($"Server response: {response.ReasonPhrase}({(int)response.StatusCode}) => {response.Content.Headers.ContentType} in {requestWatch.ElapsedMilliseconds}ms");

                Stopwatch deserializerWatch = new Stopwatch();
                deserializerWatch.Start();
                string bodyAsText = await response.Content.ReadAsStringAsync();
                deserializerWatch.Stop();
                logger?.LogInformation($"Deserialization completed successfully in {deserializerWatch.ElapsedMilliseconds}ms");


                logger?.LogTrace($"Server response body: { bodyAsText }");
                response.EnsureSuccessStatusCode();


                return bodyAsText;
            });
        }

        internal IObservable<TResponse> Request<TResponse>()
            where TResponse : class
        {
            logger?.LogTrace("Applying the options");
            Setup();
            logger?.LogTrace("The options were applied, executing the request");

            return SingleObservable.Create(async () =>
            {
                var uri = GetUri();
                logger?.LogInformation($"{MethodName.ToUpper()} {uri.AbsoluteUri}");


                logger?.LogTrace("Getting the RequestMediaType");

                HttpContent httpContent = null;

                if(obj != null)
                {
                    if (obj is HttpContent)
                    {
                        httpContent = obj as HttpContent;
                    }
                    else
                    {
                        if (RequestMediaType == null)
                        {
                            logger?.LogTrace("RequestMediaType is null, using the default (application/json)");
                            RequestMediaType = MediaTypesMap.GetMediaType(MediaType.Application.Json);
                        }

                        Stopwatch serializeWatch = new Stopwatch();
                        serializeWatch.Start();
                        httpContent = RequestMediaType.Serialize(this.obj);
                        serializeWatch.Stop();
                        logger?.LogInformation($"Serialization completed successfully in {serializeWatch.ElapsedMilliseconds}ms");
                        logger?.LogTrace($"Request content: {await httpContent.ReadAsStringAsync()}");
                    }
                }


                Stopwatch requestWatch = new Stopwatch();
                requestWatch.Start();
                var response = await DoRequest(uri.AbsoluteUri, httpContent);
                requestWatch.Stop();

                logger?.LogInformation($"Server response: {response.ReasonPhrase}({(int)response.StatusCode}) => {response.Content.Headers.ContentType} in {requestWatch.ElapsedMilliseconds}ms");

                logger?.LogTrace($"Server response body: { await response.Content.ReadAsStringAsync() }");
                response.EnsureSuccessStatusCode();

                logger?.LogTrace("Getting the ResponseMediaType");
                if (ResponseMediaType == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    logger?.LogTrace($"ResponseMediaType is null, trying to use the mime type from the server({mimeType})");
                    ResponseMediaType = MediaTypesMap.GetMediaType(mimeType);
                }

                Stopwatch deserializerWatch = new Stopwatch();
                deserializerWatch.Start();
                var responseObject = ResponseMediaType.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
                deserializerWatch.Stop();
                logger?.LogInformation($"Deserialization completed successfully in {deserializerWatch.ElapsedMilliseconds}ms");

                return responseObject;
            });
        }
    }
}