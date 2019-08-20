using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Serializers;

namespace Rx.Http
{
    public class RxHttpRequest
    {
        private HttpClient http;

        public RxHttpRequest()
        {
            this.http = new HttpClient();
        }

        public RxHttpRequest(HttpClient http)
        {
            this.http = http;
        }

        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> func = null) 
            where TResponse: class
        {
            return ExecuteRequest<TResponse>(() => http.GetAsync(url), func);
        }

        public IObservable<TResponse> Post<TResponse>(string url, object content, Action<RxHttpRequestOptions> func = null)
            where TResponse: class
        {
            return ExecuteRequest<TResponse>(() => http.PostAsync(url, new ByteArrayContent(new byte[]{})), func);
        }
        
        public IObservable<TResponse> Post<TResponse>(string url, HttpContent content, Action<RxHttpRequestOptions> func = null) where TResponse: class
        {
            return ExecuteRequest<TResponse>(() => http.PostAsync(url, content), func);
        }
        
        private IObservable<TResponse> ExecuteRequest<TResponse>(Func<Task<HttpResponseMessage>> method, Action<RxHttpRequestOptions> func) 
            where TResponse: class
        {
            return SingleObservable.Create(async () => 
            {
                var options = new RxHttpRequestOptions(); 
                func?.Invoke(options);
                var response = await method.Invoke();

                /*
                 * At this point, we need to deserialize the response from server
                 * Check if a Deserializer is set. 
                 * If it is, then use it. 
                 * Otherwise, try to get a default serializer using mime type from response
                 */
                if (options.Deserializer == null)
                {
                    HttpMediaType FindMimeTypeFromContentType()
                    {
                        try
                        {
                            var mimeType = response.Content.Headers.ContentType.MediaType;   
                            return MediaTypesMap.GetMediaType(mimeType);
                        }
                        catch
                        {
                            return null;
                        }
                    }

                    options.Deserializer = FindMimeTypeFromContentType()?.Serializer ?? new TextSerializer();
                }

                return options.Deserializer.Deserialize<TResponse>(await response.Content.ReadAsStreamAsync());
            });
        }
    }
}