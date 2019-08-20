using System;
using System.Net.Http;
using Rx.Http.MediaTypes;

namespace Rx.Http.Requests
{
    public class RxGetHttpRequest : RxHttpRequest
    {
        public RxGetHttpRequest(string url, Action<RxHttpRequestOptions> opts = null) : base(url, opts, null)
        {
        }

        internal override IObservable<TResponse> Execute<TResponse>(HttpClient http)
        {
            return SingleObservable.Create(async () => 
            {
                var response = await http.GetAsync(Url);
                
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