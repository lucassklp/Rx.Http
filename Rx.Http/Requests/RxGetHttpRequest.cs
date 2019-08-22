using System;
using System.Net.Http;
using Rx.Http.MediaTypes;
using Rx.Http.Serializers;

namespace Rx.Http.Requests
{
    public class RxGetHttpRequest : RxHttpRequest
    {
        public RxGetHttpRequest(HttpClient http, string url, Action<RxHttpRequestOptions> opts = null) : base(http, url, opts, null)
        {
        }

        internal override IObservable<string> Request()
        {
            return SingleObservable.Create(async () =>
            {
                var response = await http.GetAsync(GetUri());
                Deserializer = new TextSerializer();
                return Deserializer.Deserialize<string>(await response.Content.ReadAsStreamAsync());
            });
        }

        internal override IObservable<TResponse> Request<TResponse>()
        {
            return SingleObservable.Create(async () => 
            {
                var response = await http.GetAsync(GetUri());
                
                if(Deserializer == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    var mediaType = MediaTypesMap.GetMediaType(mimeType);
                    Deserializer = mediaType.BodySerializer;
                }

                return Deserializer.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
            });
        }
    }
}