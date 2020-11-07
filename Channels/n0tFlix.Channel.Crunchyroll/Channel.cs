using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Helpers.YoutubeDL.YoutubeDL;
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

namespace n0tFlix.Channel.Crunchyroll
{
    public class Channel : IChannel, IRequiresMediaInfoCallback
    {
        #region Just some variables that uses the Plugin.cs file variables

        public string Name => Plugin.Instance.Name;
        public string Description => Plugin.Instance.Description;
        public string DataVersion => Plugin.Instance.Version.ToString();
        public string HomePageUrl => Plugin.Instance.HomePageURL;
        public string Key => Plugin.Instance.Name;
        public string Category => Plugin.Instance.Name;

        #endregion Just some variables that uses the Plugin.cs file variables

        #region Access controller function/settings

        /// <summary>
        /// Gotta be kid friendly here now
        /// </summary>
        public ChannelParentalRating ParentalRating => ChannelParentalRating.GeneralAudience;

        /// <summary>
        /// Checks if the user has access to the channel
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsEnabledFor(string userId)
        {
            //todo make a user access check system here
            return true;
        }

        #endregion Access controller function/settings

        private readonly ILogger logger;
        private readonly Worker worker;
        private IMemoryCache memoryCache;

        public Channel(ILogger<Channel> logger, IMemoryCache memoryCache)
        {
            this.logger = logger;
            worker = new Worker();
            this.memoryCache = memoryCache;
            this.logger.LogInformation(GetType().Namespace + " initialised and ready for use");
        }

        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures()
            {
                MediaTypes = new List<MediaBrowser.Model.Channels.ChannelMediaType>()
                {
                    MediaBrowser.Model.Channels.ChannelMediaType.Video,
                    MediaBrowser.Model.Channels.ChannelMediaType.Audio,
                    MediaBrowser.Model.Channels.ChannelMediaType.Photo,
                },
                ContentTypes = new List<MediaBrowser.Model.Channels.ChannelMediaContentType>()
                 {
                    MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Episode,
                      MediaBrowser.Model.Channels.ChannelMediaContentType.Movie,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Podcast,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Song,
                     MediaBrowser.Model.Channels.ChannelMediaContentType.Trailer
                }, //<=== just adding all to have them, probably only need one but hey we can remove that when we get there
                DefaultSortFields = new List<MediaBrowser.Model.Channels.ChannelItemSortField>()
                 {
                      MediaBrowser.Model.Channels.ChannelItemSortField.Name,
                       MediaBrowser.Model.Channels.ChannelItemSortField.DateCreated,
                        MediaBrowser.Model.Channels.ChannelItemSortField.Runtime,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityPlayCount,
                     MediaBrowser.Model.Channels.ChannelItemSortField.CommunityRating,
                     MediaBrowser.Model.Channels.ChannelItemSortField.PremiereDate,
                      MediaBrowser.Model.Channels.ChannelItemSortField.PlayCount
                 }, //<== again adding all, probably dont need it, but hey the luxuary problems in life =D
                MaxPageSize = 25, //<== this one here is n0t working, i downloaded 1000s of youtube pages at once and it became a big a list without any more pages :(
                SupportsContentDownloading = true,
                SupportsSortOrderToggle = true,
                AutoRefreshLevels = 4, //<== i would love to know whats a good value to put here bro
            };
        }

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.FolderId))
                return await worker.GetCategories(logger);
            if (query.FolderId.StartsWith("category"))
                return await worker.GetCategoryItems(logger, query);
            if (query.FolderId.StartsWith("serie"))
                return await worker.GetSerieItems(logger, query);
            return default;
        }

        #region Channel image configuration

        /// <summary>
        /// 850width and 475 Height are the dimensions that give me the best logo here, else its gonna be all kinds of coco
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case ImageType.Thumb: //Gives back your logo png from the ManifestResource
                    {
                        var path = GetType().Namespace + ".Images.logo.png";

                        return await Task.FromResult(new DynamicImageResponse
                        {
                            Format = ImageFormat.Png,
                            HasImage = true,

                            Stream = GetType().Assembly.GetManifestResourceStream(path)
                        });
                    }
                default:
                    throw new ArgumentException("Unsupported image type: " + type);
            }
        }

        /// <summary>
        /// This beutifull little bastard tells us what kind of pictures to support,
        /// you should probably enable more types, im just lazy
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ImageType> GetSupportedChannelImages()
            => new List<ImageType>
            {
                ImageType.Thumb
    };

        #endregion Channel image configuration

        public async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, CancellationToken cancellationToken)
        {
            string source = await GetResponse(id);
            string json = source.Substring(source.IndexOf("vilos.config.media =") + "vilos.config.media =".Length);
            json = json.Substring(0, json.IndexOf("};") + 1);

            StreamJsonResults.root root = StreamJsonResults.GetFromJsonString(json);
            List<MediaStream> list = new List<MediaStream>();
            logger.LogInformation(Plugin.Instance.Configuration.URL + "/YoutubeDL/DownloadInfo?url=" + id);
            string st = await GetResponse(Plugin.Instance.Configuration.URL + "/YoutubeDL/DownloadInfo?url=" + id);
            logger.LogInformation(st);
            //Todo get the mediasource for selected content
            return new List<MediaSourceInfo>() { new MediaSourceInfo() {
             Name = root.Metadata.Title,
              Id = Guid.NewGuid().ToString(),
              Path = st,
          //    MediaStreams = list,
               EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
               Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,

               IsRemote = true
            } };
        }

        public async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ChannelLatestMediaSearch request, CancellationToken cancellationToken)
        {
            //Todo get the latest contect awailable to show on the start page
            return default;
        }

        private static readonly HttpClient _HttpClient = new HttpClient();

        private static async Task<string> GetResponse(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                using (var response = await _HttpClient.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
        }
    }
}