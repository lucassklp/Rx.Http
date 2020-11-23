using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using HtmlAgilityPack;

namespace Rx.Http.Extensions
{
    public static class RxHttpResponseMessageExtensions
    {
        public static IObservable<string> AsString(this IObservable<HttpResponseMessage> response)
        {
            return response.SelectMany(async (httpResp) =>
            {
                return await httpResp.Content.ReadAsStringAsync();
            });
        }

        public static IObservable<HttpResponseMessage> ToFile(this IObservable<HttpResponseMessage> response, string path)
        {
            return response.SelectMany(async (httpResp) =>
            {
                var fileStream = File.Create(path);
                var stream = await httpResp.Content.ReadAsStreamAsync();
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                fileStream.Close();
                return httpResp;
            });
        }

        public static IObservable<HtmlDocument> AsHtmlDocument(this IObservable<HttpResponseMessage> response)
        {
            return response.SelectMany(async httpResponse =>
            {
                var html = await httpResponse.Content.ReadAsStringAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                return htmlDoc;
            });
        }
    }
}