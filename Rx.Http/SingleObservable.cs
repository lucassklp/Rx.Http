using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Http
{
    public static class SingleObservable
    {
        public static IObservable<T> Create<T>(Func<T> func)
        {
            return Observable.Create<T>(observer =>
            {
                try
                {
                    observer.OnNext(func.Invoke());
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
                finally
                {
                    observer.OnCompleted();
                }

                return Disposable.Empty;
            });
        }

        public static IObservable<T> Create<T>(Func<Task<T>> task)
        {
            return Observable.Create<T>(async observer =>
            {
                try
                {
                    observer.OnNext(await task());
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
                finally
                {
                    observer.OnCompleted();
                }

                return Disposable.Empty;
            });
        }
    }
}
