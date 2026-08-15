using System;
using System.Text;

namespace Rx.Http.Extensions
{
    public static class RxHttpRequestOptionsExtensions
    {
        private const string AuthorizationHeader = "Authorization";

        public static RxHttpRequestOptions UseBasicAuthorization(this RxHttpRequestOptions options, string user, string key)
        {
            var token = $"{user}:{key}";
            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            return options.AddHeader(AuthorizationHeader, $"Basic {tokenBase64}");
        }

        public static RxHttpRequestOptions UseBearerAuthorization(this RxHttpRequestOptions options, string token)
        {
            return options.AddHeader(AuthorizationHeader, $"Bearer {token}");
        }
    }
}
