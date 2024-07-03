using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using Rx.Http.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        public static RxHttpClient Create() => new RxHttpClient(new HttpClient(), null);

        private readonly HttpClient httpClient;
        private RxHttpLogger logger;

        public List<RxRequestInterceptor> RequestInterceptors { get; private set; }
        public List<RxResponseInterceptor> ResponseInterceptors { get; private set; }

        public RxHttpClient(HttpClient httpClient, RxHttpLogger logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            this.RequestInterceptors = new List<RxRequestInterceptor>();
            this.ResponseInterceptors = new List<RxResponseInterceptor>();
        }

        public RxHttpClient UseLogger(RxHttpLogger logger)
        {
            this.logger = logger;
            return this;
        }

        public IObservable<RxHttpResponse> Get(string url)
        {
            return Request(url, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, object content)
        {
            return Request(url, content, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, Action<RxHttpRequestOptions> options)
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

        public IObservable<RxHttpResponse> Post(string url)
        {
            return Request(url, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, object content)
        {
            return Request(url, content, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, Action<RxHttpRequestOptions> options)
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

        public IObservable<RxHttpResponse> Patch(string url)
        {
            return Request(url, HttpMethod.Patch);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content)
        {
            return Request(url, content, HttpMethod.Patch);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Patch);
        }

        public IObservable<RxHttpResponse> Patch(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Patch);
        }

        public IObservable<T> Patch<T>(string url)
        {
            return Request<T>(url, HttpMethod.Patch);
        }

        public IObservable<T> Patch<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Patch);
        }

        public IObservable<T> Patch<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Patch);
        }

        public IObservable<T> Patch<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Patch);
        }

        public IObservable<RxHttpResponse> Put(string url)
        {
            return Request(url, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, object content)
        {
            return Request(url, content, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, Action<RxHttpRequestOptions> options)
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

        public IObservable<RxHttpResponse> Delete(string url)
        {
            return Request(url, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content)
        {
            return Request(url, content, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, Action<RxHttpRequestOptions> options)
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
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method);
        }

        public IObservable<T> Request<T>(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method);
        }

        public IObservable<T> Request<T>(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method);
        }

        public IObservable<T> Request<T>(string url, object obj, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method);
        }

        public IObservable<RxHttpResponse> Request(string url, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method);
        }

        public IObservable<RxHttpResponse> Request(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method);
        }

        private IObservable<RxHttpResponse> Request(RxHttpRequest httpRequest, HttpMethod method)
        {
            return SingleObservable.Create(async () =>
            {
                var requestId = Guid.NewGuid();
                httpRequest.RequestInterceptors.ForEach(interceptor => interceptor.Intercept(httpRequest));
                var message = BuildRequestMessage(httpRequest, method);
                var url = message.RequestUri.AbsoluteUri;
                logger?.OnSend(message, requestId);
                var response = await httpClient.SendAsync(message);
                logger?.OnReceive(response, url, message.Method, requestId);
                httpRequest.ResponseInterceptors.ForEach(interceptor => interceptor.Intercept(response));
                return new RxHttpResponse(response, httpRequest);
            });
        }


        public IObservable<T> Request<T>(RxHttpRequest httpRequest, HttpMethod method)
        {
            return Request(httpRequest, method)
                .Content<T>();
        }

        private string BuildUrl(RxHttpRequest request)
        {
            var builder = new UriBuilder((httpClient.BaseAddress?.AbsoluteUri ?? string.Empty) + request.Url);

            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var entry in request.QueryStrings)
            {
                foreach(var param in entry.Value)
                {
                    query.Add(entry.Key, param);
                }
            }

            builder.Query = query.ToString();
            return builder.Uri.AbsoluteUri;
        }

        private HttpRequestMessage BuildRequestMessage(RxHttpRequest request, HttpMethod method)
        {
            var url = BuildUrl(request);
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