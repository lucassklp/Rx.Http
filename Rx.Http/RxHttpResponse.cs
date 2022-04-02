using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Rx.Http
{
    public class RxHttpResponse
    {
        public RxHttpRequest Request { get; private set; }
        private HttpResponseMessage httpResponse;

        public HttpStatusCode StatusCode => httpResponse.StatusCode;
        public HttpContent Content => httpResponse.Content;
        public bool IsSuccessStatusCode => httpResponse.IsSuccessStatusCode;
        public string ReasonPhrase => httpResponse.ReasonPhrase;
        public HttpResponseHeaders Headers => httpResponse.Headers;
        public HttpRequestMessage RequestMessage => httpResponse.RequestMessage;
        public HttpResponseHeaders TrailingHeaders => httpResponse.TrailingHeaders;
        public Version Version => httpResponse.Version;

        public HttpResponseMessage EnsureSuccessStatusCode() => httpResponse.EnsureSuccessStatusCode();
        public RxHttpResponse(HttpResponseMessage httpResponse, RxHttpRequest request)
        {
            Request = request;
            this.httpResponse = httpResponse;
        }

        public void Dispose()
        {
            this.httpResponse.Dispose();
        }
    }
}
