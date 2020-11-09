using System;
using Rx.Http.MediaTypes;

namespace Rx.Http.Extensions
{
    public static class RxMediaTypeExtensions
    {
        public static RxHttpRequestOptions UseJsonMediaType(this RxHttpRequestOptions options)
        {
            options.SetRequestMediaType(new JsonHttpMediaType(RxHttp.Default.Serializable));
            return options;
        }
    }
}
