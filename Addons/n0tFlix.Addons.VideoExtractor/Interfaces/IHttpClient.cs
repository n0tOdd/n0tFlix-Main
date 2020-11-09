using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IHTTPClient : IDisposable
    {
        Task<IResponse> DoRequest(IRequest request);

        void SetRequestTimeout(TimeSpan timeout);
    }
}