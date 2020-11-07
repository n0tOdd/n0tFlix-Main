using MediaBrowser.Common;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Subtitles.Name
{
    internal class SubtitleDownloader : ISubtitleProvider
    {
        private readonly ILogger<SubtitleDownloader> _logger;

        public SubtitleDownloader(ILogger<SubtitleDownloader> logger)
        {
            _logger = logger;
        }

        private PluginConfiguration GetOptions()
        {
            return Plugin.Instance.Configuration;
        }

        public string Name => Plugin.Instance.Name;

        public IEnumerable<VideoContentType> SupportedMediaTypes => throw new NotImplementedException();

        public Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}