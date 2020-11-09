using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IProxyConfig
    {
        string Host { get; }
        int Port { get; }
        string? User { get; }
        string? Password { get; }
        bool SkipSSLCheck { get; }

        /// <summary>
        ///   Whether to bypass the proxy server for local addresses.
        /// </summary>
        bool BypassProxyOnLocal { get; }
    }
}