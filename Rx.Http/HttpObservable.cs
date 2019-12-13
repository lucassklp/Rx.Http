using Rx.Http.MediaTypes;
using Rx.Http.Requests;
using System;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace Rx.Http
{
    public class HttpObservable<T> : IObservable<T>
        where T : class
    {
        private RxHttpRequest request;
        private Task<HttpResponseMessage> task;

        public HttpObservable(RxHttpRequest request)
        {
            this.request = request;
            this.request.RequestInterceptors.ForEach(x => x.Intercept(request));
            this.task = request.CreateRequest();
        }

        public IDisposable Cancel()
        {
            request.CancellationToken.Cancel();
            return Disposable.Empty;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            task.GetAwaiter().OnCompleted(async () =>
            {
                var response = task.Result;
                request.ResponseInterceptors.ForEach(x => x.Intercept(response));
                if (request.ResponseMediaType == null)
                {
                    var mimeType = response.Content.Headers.ContentType.MediaType;
                    request.ResponseMediaType = MediaTypesMap.Get(mimeType);
                }

                var responseObject = request.ResponseMediaType.Deserialize<T>(await response.Content.ReadAsStreamAsync());
                observer.OnNext(responseObject);
                observer.OnCompleted();
            });

            return Disposable.Empty;
        }
    }
}