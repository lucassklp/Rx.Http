using Rx.Http.Extensions;
using Rx.Http.Interceptors;
using Rx.Http.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Web;

namespace Rx.Http
{
    public class RxHttpClient : IDisposable
    {
        public static RxHttpClient Create() => new RxHttpClient(new HttpClient(), null);

#if NETSTANDARD2_0
        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");
#else
        private static readonly HttpMethod PatchMethod = HttpMethod.Patch;
#endif

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

        public IObservable<RxHttpResponse> Get(string url, CancellationToken cancellationToken)
        {
            return Request(url, HttpMethod.Get, cancellationToken);
        }

        public IObservable<RxHttpResponse> Get(string url, object content)
        {
            return Request(url, content, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, object content, CancellationToken cancellationToken)
        {
            return Request(url, content, HttpMethod.Get, cancellationToken);
        }

        public IObservable<RxHttpResponse> Get(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, content, options, HttpMethod.Get, cancellationToken);
        }

        public IObservable<RxHttpResponse> Get(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Get);
        }

        public IObservable<RxHttpResponse> Get(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, options, HttpMethod.Get, cancellationToken);
        }

        public IObservable<T> Get<T>(string url)
        {
            return Request<T>(url, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, CancellationToken cancellationToken)
        {
            return Request<T>(url, HttpMethod.Get, cancellationToken);
        }

        public IObservable<T> Get<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, HttpMethod.Get, cancellationToken);
        }

        public IObservable<T> Get<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, options, HttpMethod.Get, cancellationToken);
        }

        public IObservable<T> Get<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Get);
        }

        public IObservable<T> Get<T>(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, options, HttpMethod.Get, cancellationToken);
        }

        public IObservable<RxHttpResponse> Post(string url)
        {
            return Request(url, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, CancellationToken cancellationToken)
        {
            return Request(url, HttpMethod.Post, cancellationToken);
        }

        public IObservable<RxHttpResponse> Post(string url, object content)
        {
            return Request(url, content, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, object content, CancellationToken cancellationToken)
        {
            return Request(url, content, HttpMethod.Post, cancellationToken);
        }

        public IObservable<RxHttpResponse> Post(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, content, options, HttpMethod.Post, cancellationToken);
        }

        public IObservable<RxHttpResponse> Post(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Post);
        }

        public IObservable<RxHttpResponse> Post(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, options, HttpMethod.Post, cancellationToken);
        }

        public IObservable<T> Post<T>(string url)
        {
            return Request<T>(url, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, CancellationToken cancellationToken)
        {
            return Request<T>(url, HttpMethod.Post, cancellationToken);
        }

        public IObservable<T> Post<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, HttpMethod.Post, cancellationToken);
        }

        public IObservable<T> Post<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, options, HttpMethod.Post, cancellationToken);
        }

        public IObservable<T> Post<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Post);
        }

        public IObservable<T> Post<T>(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, options, HttpMethod.Post, cancellationToken);
        }

        public IObservable<RxHttpResponse> Patch(string url)
        {
            return Request(url, PatchMethod);
        }

        public IObservable<RxHttpResponse> Patch(string url, CancellationToken cancellationToken)
        {
            return Request(url, PatchMethod, cancellationToken);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content)
        {
            return Request(url, content, PatchMethod);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content, CancellationToken cancellationToken)
        {
            return Request(url, content, PatchMethod, cancellationToken);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, PatchMethod);
        }

        public IObservable<RxHttpResponse> Patch(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, content, options, PatchMethod, cancellationToken);
        }

        public IObservable<RxHttpResponse> Patch(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, PatchMethod);
        }

        public IObservable<RxHttpResponse> Patch(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, options, PatchMethod, cancellationToken);
        }

        public IObservable<T> Patch<T>(string url)
        {
            return Request<T>(url, PatchMethod);
        }

        public IObservable<T> Patch<T>(string url, CancellationToken cancellationToken)
        {
            return Request<T>(url, PatchMethod, cancellationToken);
        }

        public IObservable<T> Patch<T>(string url, object content)
        {
            return Request<T>(url, content, PatchMethod);
        }

        public IObservable<T> Patch<T>(string url, object content, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, PatchMethod, cancellationToken);
        }

        public IObservable<T> Patch<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, PatchMethod);
        }

        public IObservable<T> Patch<T>(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, options, PatchMethod, cancellationToken);
        }

        public IObservable<T> Patch<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, PatchMethod);
        }

        public IObservable<T> Patch<T>(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, options, PatchMethod, cancellationToken);
        }

        public IObservable<RxHttpResponse> Put(string url)
        {
            return Request(url, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, CancellationToken cancellationToken)
        {
            return Request(url, HttpMethod.Put, cancellationToken);
        }

        public IObservable<RxHttpResponse> Put(string url, object content)
        {
            return Request(url, content, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, object content, CancellationToken cancellationToken)
        {
            return Request(url, content, HttpMethod.Put, cancellationToken);
        }

        public IObservable<RxHttpResponse> Put(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, content, options, HttpMethod.Put, cancellationToken);
        }

        public IObservable<RxHttpResponse> Put(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Put);
        }

        public IObservable<RxHttpResponse> Put(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, options, HttpMethod.Put, cancellationToken);
        }


        public IObservable<T> Put<T>(string url)
        {
            return Request<T>(url, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, CancellationToken cancellationToken)
        {
            return Request<T>(url, HttpMethod.Put, cancellationToken);
        }

        public IObservable<T> Put<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, HttpMethod.Put, cancellationToken);
        }

        public IObservable<T> Put<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, options, HttpMethod.Put, cancellationToken);
        }

        public IObservable<T> Put<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Put);
        }

        public IObservable<T> Put<T>(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, options, HttpMethod.Put, cancellationToken);
        }

        public IObservable<RxHttpResponse> Delete(string url)
        {
            return Request(url, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, CancellationToken cancellationToken)
        {
            return Request(url, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content)
        {
            return Request(url, content, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content, CancellationToken cancellationToken)
        {
            return Request(url, content, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request(url, content, options, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, content, options, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<RxHttpResponse> Delete(string url, Action<RxHttpRequestOptions> options)
        {
            return Request(url, options, HttpMethod.Delete);
        }

        public IObservable<RxHttpResponse> Delete(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request(url, options, HttpMethod.Delete, cancellationToken);
        }


        public IObservable<T> Delete<T>(string url)
        {
            return Request<T>(url, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, CancellationToken cancellationToken)
        {
            return Request<T>(url, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<T> Delete<T>(string url, object content)
        {
            return Request<T>(url, content, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, object content, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<T> Delete<T>(string url, object content, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, content, options, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, object content, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, content, options, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<T> Delete<T>(string url, Action<RxHttpRequestOptions> options)
        {
            return Request<T>(url, options, HttpMethod.Delete);
        }

        public IObservable<T> Delete<T>(string url, Action<RxHttpRequestOptions> options, CancellationToken cancellationToken)
        {
            return Request<T>(url, options, HttpMethod.Delete, cancellationToken);
        }

        public IObservable<T> Request<T>(string url, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method);
        }

        public IObservable<T> Request<T>(string url, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method, cancellationToken);
        }

        public IObservable<T> Request<T>(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method);
        }

        public IObservable<T> Request<T>(string url, Action<RxHttpRequestOptions> options, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method, cancellationToken);
        }

        public IObservable<T> Request<T>(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method);
        }

        public IObservable<T> Request<T>(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method, cancellationToken);
        }

        public IObservable<T> Request<T>(string url, object obj, HttpMethod method)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method);
        }

        public IObservable<T> Request<T>(string url, object obj, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request<T>(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method, cancellationToken);
        }

        public IObservable<RxHttpResponse> Request(string url, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method);
        }

        public IObservable<RxHttpResponse> Request(string url, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors), method, cancellationToken);
        }

        public IObservable<RxHttpResponse> Request(string url, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method);
        }

        public IObservable<RxHttpResponse> Request(string url, Action<RxHttpRequestOptions> options, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, null, options), method, cancellationToken);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, Action<RxHttpRequestOptions> options, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj, options), method, cancellationToken);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, HttpMethod method)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method);
        }

        public IObservable<RxHttpResponse> Request(string url, object obj, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request(new RxHttpRequest(url, RequestInterceptors, ResponseInterceptors, obj), method, cancellationToken);
        }

        private IObservable<RxHttpResponse> Request(RxHttpRequest httpRequest, HttpMethod method)
        {
            return Request(httpRequest, method, CancellationToken.None);
        }

        private IObservable<RxHttpResponse> Request(RxHttpRequest httpRequest, HttpMethod method, CancellationToken cancellationToken)
        {
            return Observable.FromAsync(async subscriptionToken =>
            {
                using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(subscriptionToken, cancellationToken);
                var requestId = Guid.NewGuid();
                httpRequest.RequestInterceptors.ForEach(interceptor => interceptor.Intercept(httpRequest));
                var message = BuildRequestMessage(httpRequest, method);
                var url = message.RequestUri.AbsoluteUri;
                if (logger != null)
                {
                    await logger.OnSend(message, requestId);
                }
                var response = await httpClient.SendAsync(message, linkedTokenSource.Token);
                if (logger != null)
                {
                    await logger.OnReceive(response, url, message.Method, requestId);
                }
                httpRequest.ResponseInterceptors.ForEach(interceptor => interceptor.Intercept(response));
                return new RxHttpResponse(response, httpRequest);
            });
        }

        public IObservable<T> Request<T>(RxHttpRequest httpRequest, HttpMethod method)
        {
            return Request(httpRequest, method)
                .Content<T>();
        }

        public IObservable<T> Request<T>(RxHttpRequest httpRequest, HttpMethod method, CancellationToken cancellationToken)
        {
            return Request(httpRequest, method, cancellationToken)
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
