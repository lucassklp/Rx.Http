using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;

namespace Rx.Http.Extensions
{
    public static class RxHttpResponseMessageExtensions
    {
        public static IObservable<string> ToString(this IObservable<HttpResponseMessage> response)
        {
            return response.SelectMany(async (httpResp) =>
            {
                return await httpResp.Content.ReadAsStringAsync();
            });
        }

        public static IObservable<FileStream> ToFile(this IObservable<HttpResponseMessage> response, string path)
        {
            return response.SelectMany(async (httpResp) =>
            {
                var fileStream = File.Create(path);
                var stream = await httpResp.Content.ReadAsStreamAsync();
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                fileStream.Close();
                return fileStream;
            });
        }
    }
}