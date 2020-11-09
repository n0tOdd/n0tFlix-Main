using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Nettwork
{
    public class Response : n0tFlix.Addons.VideoExtractor.Interfaces.IResponse
    {
        public Response(IDictionary<string, string> headers)
        {
            Utilities.Utils.ArgumentNotNull(headers, nameof(headers));

            Headers = new ReadOnlyDictionary<string, string>(headers);
        }

        public object? Body { get; set; }

        public IReadOnlyDictionary<string, string> Headers { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string? ContentType { get; set; }
    }
}