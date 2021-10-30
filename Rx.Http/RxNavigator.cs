using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Rx.Http.Interceptors;

namespace Rx.Http
{
    public class RxNavigator : RxConsumer
    {
        private readonly Dictionary<string, HashSet<string>> cookies;


        public static RxNavigator Create() => new RxNavigator(new ConsumerContext<RxNavigator>(new HttpClient()));

        public RxNavigator(IConsumerContext<RxNavigator> context) : base(context)
        {
            context.RequestInterceptors.Add(new CookieInterceptor(this));
            context.ResponseInterceptors.Add(new SetCookieInterceptor(this));
            cookies = new Dictionary<string, HashSet<string>>();
        }

        public void SetCookies(Uri url, params string[] cookies)
        {
            if (this.cookies.ContainsKey(url.Host))
            {
                var list = GetCookies(url);
                foreach(var cookie in cookies)
                {
                    list.Add(cookie);
                }
            }
            else
            {
                this.cookies.Add(url.Host, new HashSet<string>(cookies));
            }
        }

        public HashSet<string> GetCookies(Uri uri)
        {
            return cookies.ContainsKey(uri.Host) ? cookies[uri.Host] : new HashSet<string>();
        }

        public void ClearAllCookies()
        {
            cookies.Clear();
        }

        public void ClearCookies(Uri uri)
        {
            if (cookies.ContainsKey(uri.Host))
            {
                cookies.Remove(uri.Host);
            }
        }

        new public IObservable<TResponse> Get<TResponse>(string url)
            where TResponse : class
        {
            return base.Get<TResponse>(url);
        }

        new public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Get<TResponse>(url, options);
        }

        new public IObservable<HttpResponseMessage> Get(string url)
        {
            return base.Get(url);
        }

        new public IObservable<HttpResponseMessage> Get(string url, Action<RxHttpRequestOptions> options)
        {
            return base.Get(url, options);
        }

        new public IObservable<TResponse> Post<TResponse>(string url)
            where TResponse : class
        {
            return base.Post<TResponse>(url);
        }

        new public IObservable<TResponse> Post<TResponse>(string url, object obj)
            where TResponse : class
        {
            return base.Post<TResponse>(url, obj);
        }

        new public IObservable<TResponse> Post<TResponse>(string url, object obj, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Post<TResponse>(url, obj, options);
        }

        new public IObservable<TResponse> Post<TResponse>(string url, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Post<TResponse>(url, options);
        }

        new public IObservable<HttpResponseMessage> Post(string url)
        {
            return base.Post(url);
        }

        new public IObservable<HttpResponseMessage> Post(string url, object obj)
        {
            return base.Post(url, obj);
        }

        new public IObservable<HttpResponseMessage> Post(string url, object obj, Action<RxHttpRequestOptions> options)
        {
            return base.Post(url, obj, options);
        }

        new public IObservable<HttpResponseMessage> Post(string url, Action<RxHttpRequestOptions> options)
        {
            return base.Post(url, options);
        }

        new public IObservable<TResponse> Put<TResponse>(string url)
            where TResponse : class
        {
            return base.Put<TResponse>(url);
        }

        new public IObservable<TResponse> Put<TResponse>(string url, object obj)
            where TResponse : class
        {
            return base.Put<TResponse>(url, obj);
        }

        new public IObservable<TResponse> Put<TResponse>(string url, object obj, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Put<TResponse>(url, obj, options);
        }

        new public IObservable<TResponse> Put<TResponse>(string url, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Put<TResponse>(url, options);
        }

        new public IObservable<HttpResponseMessage> Put(string url)
        {
            return base.Put(url);
        }

        new public IObservable<HttpResponseMessage> Put(string url, object obj)
        {
            return base.Put(url, obj);
        }

        new public IObservable<HttpResponseMessage> Put(string url, object obj, Action<RxHttpRequestOptions> options)
        {
            return base.Put(url, obj, options);
        }

        new public IObservable<HttpResponseMessage> Put(string url, Action<RxHttpRequestOptions> options)
        {
            return base.Put(url, options);
        }

        new public IObservable<TResponse> Delete<TResponse>(string url)
            where TResponse : class
        {
            return base.Delete<TResponse>(url);
        }

        new public IObservable<TResponse> Delete<TResponse>(string url, Action<RxHttpRequestOptions> options)
            where TResponse : class
        {
            return base.Delete<TResponse>(url, options);
        }

        new public IObservable<HttpResponseMessage> Delete(string url)
        {
            return base.Delete(url);
        }

        new public IObservable<HttpResponseMessage> Delete(string url, Action<RxHttpRequestOptions> options)
        {
            return base.Delete(url, options);
        }
    }

    internal class CookieInterceptor : RxRequestInterceptor
    {
        private RxNavigator navigator;

        public CookieInterceptor(RxNavigator navigator)
        {
            this.navigator = navigator;
        }

        public void Intercept(RxHttpRequestOptions request)
        {
            var url = new Uri(request.Url);
            var cookies = navigator.GetCookies(url);
            if (cookies.Any())
            {
                request.AddHeader(new {
                    Cookie = string.Concat("; ", cookies)
                });
            }
        }
    }

    internal class SetCookieInterceptor : RxResponseInterceptor
    {
        private readonly RxNavigator navigator;

        public SetCookieInterceptor(RxNavigator navigator)
        {
            this.navigator = navigator;
        }

        public void Intercept(HttpResponseMessage response)
        {
            if(response.Headers.Any(header => header.Key == "Set-Cookie"))
            {
                var cookies = response.Headers.GetValues("Set-Cookie")
                    .SelectMany(x => x.Split(";"))
                    .Select(x => x.Trim())
                    .ToArray();

                var uri = response.RequestMessage.RequestUri;
                navigator.SetCookies(uri, cookies);
            }
        }
    }
}