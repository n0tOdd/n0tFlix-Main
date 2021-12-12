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

namespace n0tFlix.Subtitles.Yify
{
    public class YifyDownloader : ISubtitleProvider
    {
        private readonly ILogger<YifyDownloader> _logger;
        private readonly IFileSystem _fileSystem;
        private DateTime _lastRateLimitException;
        private DateTime _lastLogin;
        private int _rateLimitLeft = 40;
        private readonly IHttpClientFactory _httpClientFactory;
        private ILocalizationManager _localizationManager;

        private readonly IServerConfigurationManager _config;

        private readonly IJsonSerializer _json;

        public YifyDownloader(ILogger<YifyDownloader> logger, IHttpClientFactory httpClientFactory, IServerConfigurationManager config, IJsonSerializer json, IFileSystem fileSystem, ILocalizationManager localizationManager)
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
            string query = request.Name + " " + request.ProductionYear;
            string source = await new HttpClient().GetStringAsync(HttpUtility.UrlEncode("https://yifysubtitles.org/search?q=" + query));

            var conf = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(conf);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var results = document.GetElementsByTagName("li");
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            foreach (IElement element in results)
            {
                var href = element.GetElementsByTagName("a").First();
                string uri = href.GetAttribute("href");
                if (string.IsNullOrEmpty(uri))
                    continue;
                string subPage = await new HttpClient().GetStringAsync(HttpUtility.UrlEncode("https://yifysubtitles.org" + uri));
                if (string.IsNullOrEmpty(subPage))
                    continue;

                var sub = await browser.OpenAsync(x => x.Content(subPage));
                var subboxes = sub.GetElementsByClassName("table other-subs").First()
                    .GetElementsByTagName("tbody").First()
                    .GetElementsByTagName("tr");
                foreach (IElement subtitle in subboxes)
                {
                    string rating = subtitle.GetElementsByClassName("rating-cell").First().TextContent;
                    string language = subtitle.GetElementsByClassName("sub-lang").First().TextContent;
                    string link = "https://yifysubtitles.org" + subtitle.GetElementsByTagName("a").First().GetAttribute("href");
                    string uploader = subtitle.GetElementsByClassName("uploader-cell").First().TextContent;
                    if (request.Language.Contains(language, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(new RemoteSubtitleInfo()
                        {
                            Author = uploader,
                            CommunityRating = float.Parse(rating),
                            Id = link + "_" + language,
                            ThreeLetterISOLanguageName = language
                        });
                    }
                }
                //Check awailable subtitles for all the results
            }
            return list;
        }
    }
}