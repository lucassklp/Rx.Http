using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Rx.Http.Interceptors;

namespace Rx.Http
{
    public class RxNavigator : RxHttpClient
    {
        private readonly Dictionary<string, HashSet<string>> cookies;

        public RxNavigator(HttpClient client) : base(client, null)
        {
            RequestInterceptors.Add(new CookieInterceptor(this));
            ResponseInterceptors.Add(new SetCookieInterceptor(this));
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
                    .Select(x => x.Trim())
                    .ToArray();

                var uri = response.RequestMessage.RequestUri;
                navigator.SetCookies(uri, cookies);
            }
        }
    }
}