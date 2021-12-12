using AngleSharp;
using AngleSharp.Dom;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Globalization;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Providers;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace n0tFlix.Subtitles.TVsubtitles
{
    public class TVsubtitlesDownloader : ISubtitleProvider
    {
        private readonly ILogger<TVsubtitlesDownloader> _logger;
        private readonly IFileSystem _fileSystem;
        private DateTime _lastRateLimitException;
        private DateTime _lastLogin;
        private int _rateLimitLeft = 40;
        private readonly IHttpClientFactory _httpClientFactory;
        private ILocalizationManager _localizationManager;

        private readonly IServerConfigurationManager _config;

        private readonly IJsonSerializer _json;

        public TVsubtitlesDownloader(ILogger<TVsubtitlesDownloader> logger, IHttpClientFactory httpClientFactory, IServerConfigurationManager config, IJsonSerializer json, IFileSystem fileSystem, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _config = config;
            _json = json;
            _fileSystem = fileSystem;
            _localizationManager = localizationManager;
        }

        public string Name => Plugin.Instance.Name;

        private PluginConfiguration GetOptions()
        {
            return Plugin.Instance.Configuration;
        }

        public IEnumerable<VideoContentType> SupportedMediaTypes
        {
            get
            {
                return new[] { VideoContentType.Movie };
            }
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            string url = id.Split("_").First();

            string source = await new HttpClient().GetStringAsync(url);
            if (string.IsNullOrEmpty(source))
                return default;
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var elem = document.GetElementsByClassName("btn-icon download-subtitle").First();
            string download = elem.GetAttribute("href");

            using (var stream = await new HttpClient().GetStreamAsync(download).ConfigureAwait(false))
            {
                ZipArchive zipArchive = new ZipArchive(stream);
                var entry = zipArchive.Entries.FirstOrDefault();
                if (entry != null)
                {
                    using (var unzippedEntryStream = entry.Open())
                    {
                        var ms = new MemoryStream();
                        await unzippedEntryStream.CopyToAsync(ms).ConfigureAwait(false);
                        return new SubtitleResponse()
                        {
                            Language = id.Split("_").Last(),
                            Stream = ms,
                            Format = "srt"
                        };
                    }
                }
            }
            return default;
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            string query = "q=" + request.Name + " " + request.ProductionYear;
            var post = new StringContent(query.Replace(" ", "+"));
            var source = await new HttpClient().PostAsync(HttpUtility.UrlEncode("http://www.tvsubtitles.net/search.php"), post);
            string cont = await source.Content.ReadAsStringAsync();
            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(cont));
            var results = document.GetElementsByClassName("margin-left:2em").First();
            var links = results.GetElementsByTagName("a");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            //todo add så den henter ut riktig episode som vi trenger
            return list;
        }
    }
}