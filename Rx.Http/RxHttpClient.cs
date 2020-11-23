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
            httpClient = http;
            this.logger = logger;
        }

        public IObservable<HttpResponseMessage> Get(string url)
        {
            return Request(url, HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, object content)
        {
            return Request(url, content, HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Get(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url)
        {
            return Request<T>(url, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Get);
        }

        public IObservable<HttpResponseMessage> Post(string url)
        {
            return Request(url, HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, object content)
        {
            return Request(url, content, HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Post(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url)
        {
            return Request<T>(url, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Post);
        }

        public IObservable<HttpResponseMessage> Put(string url)
        {
            return Request(url, HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, object content)
        {
            return Request(url, content, HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Put(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Put);
        }


        public IObservable<T> Put<T>(string url)
        {
            return Request<T>(url, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Put);
        }

        public IObservable<HttpResponseMessage> Delete(string url)
        {
            return Request(url, HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, object content)
        {
            return Request(url, content, HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Delete);
        }

        public IObservable<HttpResponseMessage> Delete(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Delete);
        }


        public IObservable<T> Delete<T>(string url)
        {
            return Request<T>(url, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Delete);
        }

        public IObservable<T> Request<T>(string url, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url), method);
        }

        public IObservable<T> Request<T>(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, null, options), method);
        }

        public IObservable<T> Request<T>(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, obj, options), method);
        }

        public IObservable<T> Request<T>(string url, object obj, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, obj), method);
        }

        public IObservable<HttpResponseMessage> Request(string url, HttpMethod method)
        {
            return Request(new RxHttpRequest(url), method);
        }

        public IObservable<HttpResponseMessage> Request(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, null, options), method);
        }

        public IObservable<HttpResponseMessage> Request(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, obj, options), method);
        }

        public IObservable<HttpResponseMessage> Request(string url, object obj, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, obj), method);
        }

        private IObservable<HttpResponseMessage> Request(RxHttpRequest request, HttpMethod method)
        {
            return SingleObservable.Create(async () =>
            {
                request.RequestInterceptors.ForEach(interceptor => interceptor.Intercept(request));
                var message = BuildRequestMessage(request, method);
                var url = message.RequestUri.AbsoluteUri;
                logger?.OnSend(message.Content, url);
                var response = await httpClient.SendAsync(message);
                logger?.OnReceive(response, url);
                request.ResponseInterceptors.ForEach(interceptor => interceptor.Intercept(response));
                try
                {
                    if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest)
                    {
                        return response.EnsureSuccessStatusCode();
                    }
                    return response;
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