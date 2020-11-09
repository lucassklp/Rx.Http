using Rx.Http.Exceptions;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Web;

namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly RxHttpLogging logger;

        public RxHttpClient(HttpClient http, RxHttpLogging logger)
        {
            this.httpClient = http;
            this.logger = logger;
        }

        public IObservable<HttpResponseMessage> Get(string url)
        {
            return Request(new RxHttpRequest(url), HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, object content)
        {
            return Request(new RxHttpRequest(url, content), HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, content, options), HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, null, options), HttpMethod.Get);
        }


        public IObservable<T> Get<T>(string url)
        {
            return Request<T>(new RxHttpRequest(url), HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content)
        {
            return Request<T>(new RxHttpRequest(url, content), HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, content, options), HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, null, options), HttpMethod.Get);
        }



        public IObservable<HttpResponseMessage> Post(string url)
        {
            return Request(new RxHttpRequest(url), HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, object content)
        {
            return Request(new RxHttpRequest(url, content), HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, content, options), HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, null, options), HttpMethod.Post);
        }


        public IObservable<T> Post<T>(string url)
        {
            return Request<T>(new RxHttpRequest(url), HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content)
        {
            return Request<T>(new RxHttpRequest(url, content), HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, content, options), HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, null, options), HttpMethod.Post);
        }


        public IObservable<HttpResponseMessage> Put(string url)
        {
            return Request(new RxHttpRequest(url), HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, object content)
        {
            return Request(new RxHttpRequest(url, content), HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, content, options), HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, null, options), HttpMethod.Put);
        }


        public IObservable<T> Put<T>(string url)
        {
            return Request<T>(new RxHttpRequest(url), HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content)
        {
            return Request<T>(new RxHttpRequest(url, content), HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, content, options), HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, null, options), HttpMethod.Put);
        }


        public IObservable<HttpResponseMessage> Delete(string url)
        {
            return Request(new RxHttpRequest(url), HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, object content)
        {
            return Request(new RxHttpRequest(url, content), HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, content, options), HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(new RxHttpRequest(url, null, options), HttpMethod.Delete);
        }


        public IObservable<T> Delete<T>(string url)
        {
            return Request<T>(new RxHttpRequest(url), HttpMethod.Put);
        }

        public IObservable<T> Delete<T>(string url, object content)
        {
            return Request<T>(new RxHttpRequest(url, content), HttpMethod.Put);
        }

        public IObservable<T> Delete<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, content, options), HttpMethod.Put);
        }

        public IObservable<T> Delete<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(new RxHttpRequest(url, null, options), HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Request(RxHttpRequest request, HttpMethod method)
        {
            return SingleObservable.Create(async () =>
            {
                request.RequestInterceptors.ForEach(interceptor => interceptor.Intercept(request));
                var message = BuildRequestMessage(request, method);
                var url = message.RequestUri.AbsoluteUri;
                logger?.OnSend(message.Content, url);
                var response = await this.httpClient.SendAsync(message);
                logger?.OnReceive(response, url);
                request.ResponseInterceptors.ForEach(interceptor => interceptor.Intercept(response));
                try
                {
                    return response.EnsureSuccessStatusCode();
                }
                catch (Exception exception)
                {
                    throw new RxHttpRequestException(response, exception);
                }
            });
        }

        public IObservable<T> Request<T>(RxHttpRequest request, HttpMethod method)
        {
            return Request(request, method)
                .SelectMany(async response => request.ResponseMediaType.Deserialize<T>(await response.Content.ReadAsStreamAsync()));
        }

        private string GetUrl(RxHttpRequest request)
        {
            var builder = new UriBuilder((httpClient.BaseAddress?.AbsoluteUri ?? string.Empty) + request.Url);

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var entry in request.QueryStrings)
            {
                query[entry.Key] = entry.Value;
            }

            builder.Query = query.ToString();

            return builder.Uri.AbsoluteUri;
        }

        private HttpRequestMessage BuildRequestMessage(RxHttpRequest request, HttpMethod method)
        {
            var url = GetUrl(request);
            var content = request.BuildContent();
            var requestMessage = new HttpRequestMessage(method, url);
            requestMessage.Content = content;
            requestMessage.Headers.Clear();
            foreach (var pair in request.Headers)
            {
                requestMessage.Headers.Add(pair.Key, pair.Value);
            }

            return requestMessage;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}