using System;
using System.IO;
using System.Reactive.Linq;
using HtmlAgilityPack;
using Rx.Http.Exceptions;

namespace Rx.Http.Extensions
{
    public static class RxHttpResponseMessageExtensions
    {
        public static IObservable<T> Content<T>(this IObservable<RxHttpResponse> response)
        {

            return response.SelectMany(async response => {
                try
                {
                    if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest)
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    var stream = await response.Content.ReadAsStreamAsync();
                    return response.Request.ResponseMediaType.Deserialize<T>(stream);
                }
                catch (Exception exception)
                {
                    throw new RxHttpRequestException(response, exception);
                }
                finally
                {
                    response.Dispose();
                }
            });
        }

        public static IObservable<string> AsString(this IObservable<RxHttpResponse> response)
        {
            return response.SelectMany(async httpResp =>
            {
                using (httpResp)
                {
                    return await httpResp.Content.ReadAsStringAsync();
                }
            });
        }

        public static IObservable<RxHttpResponse> ToFile(this IObservable<RxHttpResponse> response, string path)
        {
            return response.SelectMany(async httpResp =>
            {
                using (httpResp)
                using (var fileStream = File.Create(path))
                {
                    var stream = await httpResp.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(fileStream);
                }
                return httpResp;
            });
        }

        public static IObservable<HtmlDocument> AsHtmlDocument(this IObservable<RxHttpResponse> response)
        {
            return response.SelectMany(async httpResponse =>
            {
                using (httpResponse)
                {
                    var html = await httpResponse.Content.ReadAsStringAsync();
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);
                    return htmlDoc;
                }
            });
        }
    }
}