using System;

namespace Rx.Http.MediaTypes
{
    //Source of the list: http://www.iana.org/assignments/media-types/media-types.xhtml
    //Not using all, just the most used mime types, based on 
    //https://stackoverflow.com/questions/23714383/what-are-all-the-possible-values-for-http-content-type-header
    public static class MediaType
    {
        public static class Application
        {
            public static readonly string EDIX12 = @"application/EDI-X12";
            public static readonly string EDIFACT = @"application/EDIFACT";
            public static readonly string Javascript = @"application/javascript";
            public static readonly string OctetStream = @"application/octet-stream";
            public static readonly string Ogg = @"application/ogg";
            public static readonly string Pdf = @"application/pdf";
            public static readonly string XHtmlXml = @"application/xhtml+xml";
            public static readonly string XShockwaveFlash = @"application/x-shockwave-flash";
            public static readonly string Json = @"application/json";
            public static readonly string LdJson = @"application/ld+json";
            public static readonly string Xml = @"application/xml";
            public static readonly string Zip = @"application/zip";
            public static readonly string FormUrlEncoded = @"application/x-www-form-urlencoded";

            public static readonly string OpenDocumentText = @"application/vnd.oasis.opendocument.text";
            public static readonly string OpenDocumentSpreadsheet = @"application/vnd.oasis.opendocument.spreadsheet";
            public static readonly string OpenDocumentPresentation = @"application/vnd.oasis.opendocument.presentation";
            public static readonly string OpenDocumentGraphics = @"application/vnd.oasis.opendocument.graphics";
            public static readonly string Excel = @"application/vnd.ms-excel";
            public static readonly string OpenSpreadsheet = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public static readonly string PowerPoint = @"application/vnd.ms-powerpoint";
            public static readonly string OpenPresentation = @"application/vnd.openxmlformats-officedocument.presentationml.presentation";
            public static readonly string Word = @"application/msword";
            public static readonly string OpenDocument = @"application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            public static readonly string MozillaXulXml = @"application/vnd.mozilla.xul+xml";
        }

        public static class Audio
        {
            public static readonly string Mpeg = @"audio/mpeg";
            public static readonly string Wma = @"audio/x-ms-wma";
            public static readonly string RealAudio = @"audio/vnd.rn-realaudio";
            public static readonly string Wav = @"audio/x-wav";
        }

        public static class Image
        {
            public static readonly string Gif = @"image/gif";
            public static readonly string Jpeg = @"image/jpeg";
            public static readonly string Png = @"image/png";
            public static readonly string Tiff = @"image/tiff";
            public static readonly string MsIcon = @"image/vnd.microsoft.icon";
            public static readonly string Icon = @"image/x-icon";
            public static readonly string Djvu = @"image/vnd.djvu";
            public static readonly string Svg = @"image/svg+xml";
        }

        public static class Multipart
        {
            public static readonly string Mixed = @"multipart/mixed";
            public static readonly string Alternative = @"multipart/alternative";
            public static readonly string Related = @"multipart/related";
            public static readonly string FormData = @"multipart/form-data";
        }

        public static class Text
        {
            public static readonly string Css = @"text/css";
            public static readonly string Csv = @"text/csv";
            public static readonly string Html = @"text/html";

            [Obsolete]
            public static readonly string Javascript = @"text/javascript";

            public static readonly string Plain = @"text/plain";
            public static readonly string Xml = @"text/xml";
        }

        public static class Video
        {
            public static readonly string Mpeg = @"video/mpeg";
            public static readonly string Mp4 = @"video/mp4";
            public static readonly string Quicktime = @"video/quicktime";
            public static readonly string Wmv = @"video/x-ms-wmv";
            public static readonly string MsVideo = @"video/x-msvideo";
            public static readonly string Flv = @"video/x-flv";
            public static readonly string Webm = @"video/webm";
        }
    }
}
