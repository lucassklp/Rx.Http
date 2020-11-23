using System;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Rx.Http.Extensions
{
    public static class RxHttpRequestOptionsExtensions
    {
        public static RxHttpRequestOptions UseBasicAuthorization(this RxHttpRequestOptions options, string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            return options.AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
        }

        public static RxHttpRequestOptions UseBearerAuthorization(this RxHttpRequestOptions options, string token)
        {
            return options.AddHeader(HeaderNames.Authorization, $"Bearer {token}");
        }
    }
}
