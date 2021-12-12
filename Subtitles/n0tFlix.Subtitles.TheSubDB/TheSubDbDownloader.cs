using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using n0tFlix.Subtitles.TheSubDB.Configuration;
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
using System.Security.Cryptography;

namespace n0tFlix.Subtitles.TheSubDB
{
    public class TheSubDbDownloader : ISubtitleProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TheSubDbDownloader> _logger;
        private readonly IApplicationHost _appHost;

        public TheSubDbDownloader(ILogger<TheSubDbDownloader> logger, IHttpClientFactory httpClientFactory, IApplicationHost appHost)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _appHost = appHost;
        }

        private PluginConfiguration GetOptions()
        {
            return Plugin.Instance.Configuration;
        }

        public IEnumerable<VideoContentType> SupportedMediaTypes
        {
            get
            {
                return new[] { VideoContentType.Episode, VideoContentType.Movie };
            }
        }

        private HttpRequestMessage BaseRequestOptions(HttpMethod httpMethod, string url)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue($"Jellyfin/{_appHost?.ApplicationVersion}"));
            return request;
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            var opts = BaseRequestOptions(HttpMethod.Get, "http://api.thesubdb.com/?action=download&hash=" + id);
            _logger.LogDebug("Requesting {0}", opts.RequestUri);

            using (var response = await _httpClientFactory.CreateClient().SendAsync(opts).ConfigureAwait(false))
            {
                var ms = new MemoryStream();

                await response.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;

                IEnumerable<string> cd;
                response.Headers.TryGetValues("Content-Disposition", out cd);

                foreach (string s in cd)
                {
                    var fileExt = (s ?? string.Empty).Split('.').LastOrDefault();

                    if (string.IsNullOrWhiteSpace(fileExt))
                    {
                        fileExt = "srt";
                    }

                    return new SubtitleResponse
                    {
                        Format = fileExt,
                        Language = id.Split('=').LastOrDefault(),
                        Stream = ms
                    };
                }
                return new SubtitleResponse();
            }
        }

        public string Name => "TheSubDB";

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request,
            CancellationToken cancellationToken)
        {
            var hash = await GetHash(request.MediaPath, cancellationToken);
            var opts = BaseRequestOptions(HttpMethod.Get, "http://api.thesubdb.com/?action=search&hash=" + hash);
            _logger.LogDebug("Requesting {0}", opts.RequestUri);

            try
            {
                using (var response = await _httpClientFactory.CreateClient().SendAsync(opts).ConfigureAwait(false))
                {
                    using (var reader = new StreamReader(response.Content.ReadAsStream()))
                    {
                        var result = await reader.ReadToEndAsync().ConfigureAwait(false);
                        _logger.LogDebug("Search for subtitles for {0} returned {1}", hash, result);
                        return result
                            .Split(',')
                            .Where(lang => string.Equals(request.TwoLetterISOLanguageName, lang, StringComparison.OrdinalIgnoreCase)) //TODO: use three letter code
                            .Select(lang => new RemoteSubtitleInfo
                            {
                                IsHashMatch = true,
                                ProviderName = Name,
                                Id = $"{hash}&language={lang}",
                                Name = "A subtitle matched by hash"
                            }).ToList();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode.HasValue && ex.StatusCode.Value == HttpStatusCode.NotFound)
                {
                    return new List<RemoteSubtitleInfo>();
                }

                throw;
            }
        }

        /// <summary>
        ///     Reads 64*1024 bytes from the start and the end of the file, combines them and returns its MD5 hash
        /// </summary>
        private async Task<string> GetHash(string path, CancellationToken cancellationToken)
        {
            const int readSize = 64 * 1024;
            var buffer = new byte[readSize * 2];
            _logger.LogDebug("Reading {0}", path);
            using (var stream = File.OpenRead(path))
            {
                await stream.ReadAsync(buffer, 0, readSize, cancellationToken);

                if (stream.Length > readSize)
                {
                    stream.Seek(-readSize, SeekOrigin.End);
                }
                else
                {
                    stream.Position = 0;
                }

                await stream.ReadAsync(buffer, readSize, readSize, cancellationToken);
            }

            var hash = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                foreach (var b in md5.ComputeHash(buffer))
                    hash.Append(b.ToString("X2"));
            }
            _logger.LogDebug("Computed hash {0} of {1}", hash.ToString(), path);
            return hash.ToString();
        }
    }
}

/*: ISubtitleProvider
    {
        private static readonly CultureInfo _usCulture = CultureInfo.ReadOnly(new CultureInfo("en-US"));
        private readonly ILogger<TheSubDbDownloader> _logger;
        private readonly IFileSystem _fileSystem;
        private DateTime _lastRateLimitException;
        private DateTime _lastLogin;
        private int _rateLimitLeft = 40;
        private readonly IHttpClient _httpClient;
        private readonly IApplicationHost _appHost;
        private ILocalizationManager _localizationManager;

        private readonly IServerConfigurationManager _config;

        private readonly IJsonSerializer _json;
        private readonly SubDBClient _client;

        public TheSubDbDownloader(ILogger<TheSubDbDownloader> logger, IHttpClient httpClient, IServerConfigurationManager config, IJsonSerializer json, IFileSystem fileSystem, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _httpClient = httpClient;

            _client = new SubDBClient(new System.Net.Http.Headers.ProductHeaderValue("Desktop-Client", "1.0"));
            _config = config;
            _json = json;
            _fileSystem = fileSystem;
            _localizationManager = localizationManager;
        }

        public int Order => 3;

        public string Name
        {
            get { return "TheSubDB"; }
        }

        private PluginConfiguration GetOptions()
        {
            return Plugin.Instance.Configuration;
        }

        public IEnumerable<VideoContentType> SupportedMediaTypes
        {
            get
            {
                return new[] { VideoContentType.Episode, VideoContentType.Movie };
            }
        }

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

        public void Dispose()
        {
            this._client.Dispose();
        }

        public async Task<IEnumerable<RemoteSubtitleInfo>> Search(SubtitleSearchRequest request, CancellationToken cancellationToken)
        {
            string hash = Helpers.Utils.GetMovieHash(request.MediaPath);
            Response resp = await _client.SearchSubtitleAsync(hash, true);
            Helpers.CsvResponseParser csvResponseParser = new CsvResponseParser();
            IReadOnlyList<Language> languages = csvResponseParser.ParseSearchSubtitle(ASCIIEncoding.UTF8.GetString((byte[])resp.Body), true);
            List<RemoteSubtitleInfo> list = new List<RemoteSubtitleInfo>();
            _logger.LogInformation("Searched for languages on the hash " + hash + " and found " + languages.Count.ToString() + " results");
            foreach (Language lang in languages)
            {
                _logger.LogInformation(lang.Name.ToString() + " " + lang.Count.ToString());
                _logger.LogInformation("Fant en sub med riktig språk");
                if (request.TwoLetterISOLanguageName == lang.Name)
                {
                    RemoteSubtitleInfo remoteSubtitleInfo = new RemoteSubtitleInfo()
                    {
                        Name = lang.Name,
                        Id = lang.Name + "-n0t-" + hash,
                    };
                    list.Add(remoteSubtitleInfo);
                }
            }
            return list;
        }

        public async Task<SubtitleResponse> GetSubtitles(string id, CancellationToken cancellationToken)
        {
            string[] info = id.Split("-n0t-");

            Response response = await _client.DownloadSubtitleAsync(info[1], info[0]).ConfigureAwait(false);

            // download failed
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.LogError("Failed to download subtitle from TheSubDB");
                return null;
            }
            byte[] buffer = (byte[])response.Body;

            return new SubtitleResponse
            {
                Format = "srt",
                Language = info[0],

                Stream = new MemoryStream(buffer)
            };
            // throw new NotImplementedException();
        }
}}*/