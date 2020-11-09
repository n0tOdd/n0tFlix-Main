using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IResponse
    {
        object? Body { get; }

        IReadOnlyDictionary<string, string> Headers { get; }

        HttpStatusCode StatusCode { get; }

        string? ContentType { get; }
    }
}