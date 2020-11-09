using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using n0tFlix.Subtitles.Podnapisi.Configuration;
using MediaBrowser.Common;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Globalization;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Net;
using MediaBrowser.Model.Providers;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace n0tFlix.Subtitles.Podnapisi
{
    public class PodnapisiDownloader : ISubtitleProvider
    {
        private static readonly CultureInfo _usCulture = CultureInfo.ReadOnly(new CultureInfo("en-US"));
        private readonly ILogger<PodnapisiDownloader> _logger;
        private readonly IFileSystem _fileSystem;
        private DateTime _lastRateLimitException;
        private DateTime _lastLogin;
        private int _rateLimitLeft = 40;
        private readonly IHttpClient _httpClient;
        private readonly IApplicationHost _appHost;
        private ILocalizationManager _localizationManager;
        public PodnapisiDownloader(ILogger<PodnapisiDownloader> logger, IFileSystem fileSystem, IHttpClient httpClient, IApplicationHost appHost, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _fileSystem = fileSystem;

            _httpClient = httpClient;
            _appHost = appHost;
            _localizationManager = localizationManager;

            //       OpenSubtitlesHandler.OpenSubtitles.SetUserAgent("jellyfin");
        }

        public string Name => "Podnapisi";

        private PluginConfiguration GetOptions()
            => Plugin.Instance.Configuration;

        public IEnumerable<VideoContentType> SupportedMediaTypes
            => new[] { VideoContentType.Episode, VideoContentType.Movie };


        private HttpRequestOptions BaseRequestOptions => new HttpRequestOptions
        {
            UserAgent = $"Jellyfin/{_appHost.ApplicationVersion}"
        };

        private string NormalizeLanguage(string language)
        {
            if (language != null)
            {
                var culture = _localizationManager.FindLanguageInfo(language);
                if (culture != null)
                {
                    return culture.ThreeLetterISOLanguageName;
                }
            }

            return language;
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            var pid = id.Split(',')[0];
            var title = id.Split(',')[1];
            var lang = id.Split(',')[2];
            var opts = BaseRequestOptions;
            opts.Url = $"https://www.podnapisi.net/{lang}/subtitles/{title}/{pid}/download";
            _logger.LogDebug("Requesting {0}", opts.Url);

            using (var response = await _httpClient.GetResponse(opts).ConfigureAwait(false))
            {
                _logger.LogDebug(response.ResponseUrl);
                var ms = new MemoryStream();
                var contentType = response.ContentType.ToLower();
                if (!contentType.Contains("zip"))
                {
                    return new SubtitleResponse()
                    {
                        Language = NormalizeLanguage(lang),
                        Stream = ms
                    };
                }

                var archive = new ZipArchive(response.Content,ZipArchiveMode.Read);

                await archive.Entries.FirstOrDefault().Open().CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;

                var fileExt = archive.Entries.FirstOrDefault().FullName.Split('.').LastOrDefault();

                if (string.IsNullOrWhiteSpace(fileExt))
                {
                    fileExt = "srt";
                }

                return new SubtitleResponse
                {
                    Format = fileExt,
                    Language = NormalizeLanguage(lang),
                    Stream = ms
                };

            }
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request,
     CancellationToken cancellationToken)
        {

            var url = new StringBuilder("https://www.podnapisi.net/subtitles/search/old?sXML=1");
            url.Append($"&sL={request.TwoLetterISOLanguageName}");
            if (request.SeriesName == null)
            {
                url.Append($"&sK={request.Name}");
            }
            else
            {
                url.Append($"&sK={request.SeriesName}");
            }
            if (request.ParentIndexNumber.HasValue)
            {
                url.Append($"&sTS={request.ParentIndexNumber}");
            }
            if (request.IndexNumber.HasValue)
            {
                url.Append($"&sTE={request.IndexNumber}");
            }
            if (request.ProductionYear.HasValue)
            {
                url.Append($"&sY={request.ProductionYear}");
            }

            var opts = BaseRequestOptions;
            opts.Url = url.ToString();
            _logger.LogDebug("Requesting {0}", opts.Url);

            try
            {
                using (var response = await _httpClient.GetResponse(opts).ConfigureAwait(false))
                {
                    using (var reader = new StreamReader(response.Content))
                    {
                        var settings = Create(false);
                        settings.CheckCharacters = false;
                        settings.IgnoreComments = true;
                        settings.DtdProcessing = DtdProcessing.Parse;
                        settings.MaxCharactersFromEntities = 1024;
                        settings.Async = true;

                        using (var result = XmlReader.Create(reader, settings))
                        {
                            return (await ParseSearch(result).ConfigureAwait(false)).OrderByDescending(i => i.DownloadCount);
                        }
                    }
                }
            }
            catch (HttpException ex)
            {
                if (ex.StatusCode.HasValue && ex.StatusCode.Value == HttpStatusCode.NotFound)
                {
                    return new List<RemoteSubtitleInfo>();
                }

                throw;
            }
        }

        private XmlReaderSettings Create(bool enableValidation)
        {
            var settings = new XmlReaderSettings();

            if (!enableValidation)
            {
                settings.ValidationType = ValidationType.None;
            }

            return settings;
        }

        public int Order => 2;

        private async Task<List<RemoteSubtitleInfo>> ParseSearch(XmlReader reader)
        {
            var list = new List<RemoteSubtitleInfo>();
            await reader.MoveToContentAsync().ConfigureAwait(false);
            await reader.ReadAsync().ConfigureAwait(false);

            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "subtitle":
                            {
                                if (reader.IsEmptyElement)
                                {
                                    await reader.ReadAsync().ConfigureAwait(false);
                                    continue;
                                }
                                using (var subReader = reader.ReadSubtree())
                                {
                                    list.Add(await ParseSubtitleList(subReader).ConfigureAwait(false));
                                }
                                break;
                            }
                        default:
                            {
                                await reader.SkipAsync().ConfigureAwait(false);
                                break;
                            }
                    }
                }
                else
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                }
            }
            return list;
        }

        private async Task<RemoteSubtitleInfo> ParseSubtitleList(XmlReader reader)
        {
            var SubtitleInfo = new RemoteSubtitleInfo
            {
                ProviderName = Name,
                Format = "srt"
            };
            await reader.MoveToContentAsync().ConfigureAwait(false);
            await reader.ReadAsync().ConfigureAwait(false);
            var id = new StringBuilder();

            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "pid":
                            {
                                id.Append($"{(await reader.ReadElementContentAsStringAsync().ConfigureAwait(false))},");
                                break;
                            }
                        case "release":
                            {
                                SubtitleInfo.Name = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);
                                break;
                            }
                        case "url":
                            {
                                id.Append($"{(await reader.ReadElementContentAsStringAsync().ConfigureAwait(false)).Split('/')[5]},");
                                break;
                            }
                        case "language":
                            {
                                var lang = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);
                                SubtitleInfo.ThreeLetterISOLanguageName = NormalizeLanguage(lang);
                                id.Append($"{lang},");
                                break;
                            }
                        case "rating":
                            {
                                SubtitleInfo.CommunityRating = await ReadFloat(reader).ConfigureAwait(false);
                                break;
                            }
                        case "downloads":
                            {
                                SubtitleInfo.DownloadCount = await ReadInt(reader).ConfigureAwait(false);
                                break;
                            }
                        default:
                            {
                                await reader.SkipAsync().ConfigureAwait(false);
                                break;
                            }
                    }
                }
                else
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                }
            }
            SubtitleInfo.Id = id.ToString();
            return SubtitleInfo;
        }

        private async Task<float?> ReadFloat(XmlReader reader)
        {
            var val = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);

            if (float.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            return null;
        }

        private async Task<int?> ReadInt(XmlReader reader)
        {
            var val = await reader.ReadElementContentAsStringAsync().ConfigureAwait(false);

            if (int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }

            return null;
        }

    }
}
