using MediaBrowser.Common;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace n0tFlix.Provider.Imvdb
{
    public class Provider : IRemoteMetadataProvider<MusicVideo, MusicVideoInfo>
    {
        public string Name => "IMVDb";

        public string ProviderName => Name;

        public string Key => Name;

        public ExternalIdMediaType? Type => ExternalIdMediaType.Track & ExternalIdMediaType.Movie;

        public string UrlFormatString => "https://imvdb.com/api/v1/{0}/search/videos?q={1}+{2}";

        private IHttpClientFactory _httpClientFactory;
        private ILogger _logger;

        //   private IXmlReaderSettingsFactory _xmlSettings;
        private string BaseUrl = "https://imvdb.com/api/v1";

        private IApplicationHost _appHost;

        public Provider(IHttpClientFactory httpClientFactory, ILogger logger, IApplicationHost appHost)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            // _xmlSettings = xmlSettings;
            _appHost = appHost;
        }

        public async Task<MetadataResult<MusicVideo>> GetMetadata(MusicVideoInfo info, CancellationToken cancellationToken)
        {
            var imvdbId = info.GetProviderId("IMVDb");

            if (string.IsNullOrEmpty(imvdbId))
            {
                var searchResults = await GetSearchResults(info, cancellationToken).ConfigureAwait(false);
                var searchResult = searchResults.FirstOrDefault();
                if (searchResult != null)
                {
                    imvdbId = searchResult.GetProviderId("IMVDb");
                }
            }
            var result = new MetadataResult<MusicVideo>();

            var apiKey = Plugin.Instance.Configuration.ApiKey;
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return result;
            }

            if (!string.IsNullOrEmpty(imvdbId))
            {
                // do lookup here by imvdb id
                var releaseResults = await GetReleaseResult(imvdbId, apiKey, cancellationToken).ConfigureAwait(false);
                var releaseResult = releaseResults.FirstOrDefault();
                if (releaseResult != null)
                {
                    result.HasMetadata = true;
                    // set properties from data
                    result.Item = new MusicVideo
                    {
                        Name = releaseResult.Name,
                        ProductionYear = releaseResult.ProductionYear,
                        Artists = releaseResult.Artists.Select(i => i.Name).ToArray()
                    };
                    result.Item.SetProviderId("IMVDb", imvdbId);
                }
            }
            return result;
        }

        public async Task<IEnumerable<RemoteSearchResult>> GetSearchResults(MusicVideoInfo searchInfo, CancellationToken cancellationToken)
        {
            var apiKey = Plugin.Instance.Configuration.ApiKey;
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return new List<RemoteSearchResult>();
            }

            var url = String.Format("{0}/search/videos?q={1}+{2}", BaseUrl, String.Join("+", searchInfo.Artists), searchInfo.Name);

            if (!string.IsNullOrWhiteSpace(url))
            {
                using (var stream = await GetResponse(url, apiKey, cancellationToken).ConfigureAwait(false))
                {
                    return GetResultsFromResponse(stream, true);
                }
            }
            return new List<RemoteSearchResult>();
        }

        public async Task<IEnumerable<RemoteSearchResult>> GetReleaseResult(String imvdbId, string apiKey, CancellationToken cancellationToken)
        {
            var url = String.Format("{0}/video/{1}", BaseUrl, imvdbId);

            if (!string.IsNullOrWhiteSpace(url))
            {
                using (var stream = await GetResponse(url, apiKey, cancellationToken).ConfigureAwait(false))
                {
                        return GetResultsFromResponse(stream, false);
                }
            }
            return new List<RemoteSearchResult>();
        }

        private List<RemoteSearchResult> GetResultsFromResponse(Stream stream, bool isSearch)
        {
            using (var oReader = new StreamReader(stream, Encoding.UTF8))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.CheckCharacters = false;
                settings.IgnoreProcessingInstructions = true;
                settings.IgnoreComments = true;

                using (var reader = XmlReader.Create(oReader, settings))
                {
                    var results = isSearch ? ReleaseResult.ParseSearch(reader) : ReleaseResult.ParseReleaseList(reader);
                    return results.Select(i =>
                    {
                        var result = new RemoteSearchResult
                        {
                            Name = i.Title,
                            ProductionYear = i.Year,
                            Artists = i.Artists.ToArray()
                        };
                        result.SetProviderId("IMVDb", i.ReleaseId);
                        return result;
                    }).ToList();
                }
            }
        }

        public Task<Stream> GetResponse(string url, string apiKey, CancellationToken cancellationToken)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/xml");
                client.DefaultRequestHeaders.Add("IMVDB-APP-KEY", apiKey);
                var response = client.GetStreamAsync(url, cancellationToken);
                return response;
            }
        }

        public Task<HttpResponseMessage> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            return _httpClientFactory.CreateClient().GetAsync(url, cancellationToken);
        }

        private class ReleaseResult
        {
            public string ReleaseId;
            public string Title;
            public int? Year;
            public List<RemoteSearchResult> Artists = new List<RemoteSearchResult>();

            public static List<ReleaseResult> ParseSearch(XmlReader reader)
            {
                reader.MoveToContent();
                reader.Read();

                // Loop through each element
                while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "results":
                                {
                                    if (reader.IsEmptyElement)
                                    {
                                        reader.Read();
                                        continue;
                                    }
                                    using (var subReader = reader.ReadSubtree())
                                    {
                                        return ParseResultsList(subReader);
                                    }
                                }
                            default:
                                {
                                    reader.Skip();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                return new List<ReleaseResult>();
            }

            private static List<ReleaseResult> ParseResultsList(XmlReader reader)
            {
                var list = new List<ReleaseResult>();

                reader.MoveToContent();
                reader.Read();

                // Loop through each element
                while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "result":
                                {
                                    if (reader.IsEmptyElement)
                                    {
                                        reader.Read();
                                        continue;
                                    }

                                    using (var subReader = reader.ReadSubtree())
                                    {
                                        var release = ParseRelease(subReader);
                                        if (release != null)
                                        {
                                            list.Add(release);
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    reader.Skip();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                return list;
            }

            public static List<ReleaseResult> ParseReleaseList(XmlReader reader)
            {
                var list = new List<ReleaseResult>();
                list.Add(ParseRelease(reader));
                return list;
            }

            private static ReleaseResult ParseRelease(XmlReader reader)
            {
                var result = new ReleaseResult();

                reader.MoveToContent();
                reader.Read();

                // http://stackoverflow.com/questions/2299632/why-does-xmlreader-skip-every-other-element-if-there-is-no-whitespace-separator

                // Loop through each element
                while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "id":
                                {
                                    result.ReleaseId = reader.ReadElementContentAsString();
                                    break;
                                }
                            case "song_title":
                                {
                                    result.Title = reader.ReadElementContentAsString();
                                    break;
                                }
                            case "year":
                                {
                                    var val = reader.ReadElementContentAsString();
                                    if (int.TryParse(val, out int year))
                                    {
                                        result.Year = year;
                                    }
                                    break;
                                }
                            case "artists":
                                {
                                    using (var subReader = reader.ReadSubtree())
                                    {
                                        var artist = ParseArtistCredit(subReader);
                                        if (artist != null)
                                        {
                                            result.Artists.Add(new RemoteSearchResult
                                            {
                                                Name = artist
                                            });
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    reader.Skip();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                return result;
            }

            private static string ParseArtistCredit(XmlReader reader)
            {
                reader.MoveToContent();
                reader.Read();

                // http://stackoverflow.com/questions/2299632/why-does-xmlreader-skip-every-other-element-if-there-is-no-whitespace-separator

                // Loop through each element
                while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "artist":
                                {
                                    using (var subReader = reader.ReadSubtree())
                                    {
                                        return ParseArtistNameCredit(subReader);
                                    }
                                }
                            default:
                                {
                                    reader.Skip();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                return null;
            }

            private static string ParseArtistNameCredit(XmlReader reader)
            {
                reader.MoveToContent();
                reader.Read();

                string name = null;

                // http://stackoverflow.com/questions/2299632/why-does-xmlreader-skip-every-other-element-if-there-is-no-whitespace-separator

                // Loop through each element
                while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "name":
                                {
                                    name = reader.ReadElementContentAsString();
                                    break;
                                }
                            default:
                                {
                                    reader.Skip();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    return null;
                }

                return name;
            }
        }
    }
}