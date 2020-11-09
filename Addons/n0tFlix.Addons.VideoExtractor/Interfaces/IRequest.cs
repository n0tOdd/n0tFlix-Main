using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IRequest
    {
        Uri BaseAddress { get; }

        Uri Endpoint { get; }

        IDictionary<string, string> Headers { get; }

        IDictionary<string, string> Parameters { get; }

        HttpMethod Method { get; }

        object? Body { get; set; }
    }
}