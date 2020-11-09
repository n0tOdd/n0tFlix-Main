using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IHTTPLogger
    {
        void OnRequest(IRequest request);

        void OnResponse(IResponse response);
    }
}