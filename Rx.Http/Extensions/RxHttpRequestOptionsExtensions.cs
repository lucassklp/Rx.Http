using System;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Rx.Http.Extensions
{
    public static class RxHttpRequestOptionsExtensions
    {
        public static RxHttpRequestOptions UseBasicAuthorization(this RxHttpRequestOptions opt, string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            opt.AddHeader(HeaderNames.Authorization, $"Basic {tokenBase64}");
            return opt;
        }

        public static RxHttpRequestOptions UseBearerAuthorization(this RxHttpRequestOptions opt, string token)
        {
            opt.AddHeader(HeaderNames.Authorization, $"Bearer {token}");
            return opt;
        }
    }
}
