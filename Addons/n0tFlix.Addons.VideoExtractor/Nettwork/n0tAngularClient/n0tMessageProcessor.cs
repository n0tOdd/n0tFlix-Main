using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace n0tFlix.Addons.VideoExtractor.Nettwork.n0tAngularClient
{
    public class n0tMessageProcessor : MessageProcessingHandler
    {
        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

            //Her kan du edite requesten føre den sendes ut
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            //her kan du sjekke responsen fra serveren føre clienten får den, perfekt for å sjekke mimetypene etter lyd/bilde håper vi
            var contentType = response.Content.Headers.GetValues("Content-Type").Where(x => !string.IsNullOrEmpty(ParseMimeType(x))); //todo add en parser som sjekker alle kjente content-typer
            if (contentType.Count() != 0)
            {
                //Her er det en media fil
            }
            return response;
        }

        public string ParseMimeType(string type)
        {
            if (string.IsNullOrEmpty(type)) return string.Empty;
            if (type == "application/x-mpegURL") return "x-mpegUR";

            if (type.Contains("video/mp4")) return "mp4";
            if (type.Contains("video/iso.segment")) return "m4s";
            if (type.Contains("audio/mp4")) return "m4a";

            if (type.Contains("application/f4m")) return "f4m";
            if (type.Contains("video/f4f")) return "f4f";

            if (type.Contains("video/webm")) return "video/webm";
            if (type.Contains("audio/webm")) return "audio/webm";
            if (type.Contains("video/3gpp")) return "video/3gpp";
            if (type.Contains("hds")) return "hds";
            if (type.Contains("hls")) return "hls";
            if (type.Contains("video/hls")) return "hls";
            if (type.Contains("mp3")) return "mp3";

            Console.WriteLine("Mimetype not a known multimedia type " + type);
            return string.Empty;
        }
    }
}