using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Net;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static n0tFlix.Channel.Redtube.Models.Info;

namespace n0tFlix.Channel.Redtube
{
    public class Channel : IChannel, IRequiresMediaInfoCallback, ISupportsLatestMedia
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMemoryCache memoryCache;
        public ChannelParentalRating ParentalRating => ChannelParentalRating.Adult;
        private readonly IHttpClientFactory _httpClientFactory;

        public Channel(IHttpClientFactory httpClient, IJsonSerializer jsonSerializer, ILogger<Channel> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            this.jsonSerializer = jsonSerializer;
            this.memoryCache = memoryCache;
        }

        public string Name { get { return Plugin.Instance.Name; } }
        public string HomePageUrl { get { return "https://www.redtube.com/"; } }

        public string DataVersion
        {
            get
            {
                // Increment as needed to invalidate all caches
                return "1.0.0.0";
            }
        }

        public string Description
        {
            get { return Plugin.Instance.Description; }
        }

        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures
            {
                ContentTypes = new List<ChannelMediaContentType>
                {
                    ChannelMediaContentType.Clip
                },

                MediaTypes = new List<ChannelMediaType>
                {
                    ChannelMediaType.Video
                },
                MaxPageSize = 20,
                SupportsContentDownloading = true,
                AutoRefreshLevels = 10,
                DefaultSortFields = new List<ChannelItemSortField>()
                {
                    ChannelItemSortField.CommunityRating,
                   ChannelItemSortField.DateCreated,
                    ChannelItemSortField.Name,
                     ChannelItemSortField.PremiereDate,
                      ChannelItemSortField.Runtime,
                },
                SupportsSortOrderToggle = true
            };
        }

        public bool IsEnabledFor(string userId)
        {
            return true;
        }

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("cat ID : " + query.FolderId);
            if (query.FolderId == null)
            {
                if (memoryCache.TryGetValue("categories", out ChannelItemResult ii))
                {
                    return ii;
                }
                return await GetCategories(cancellationToken).ConfigureAwait(false);
            }
            var catSplit = query.FolderId.Split('_');
            query.FolderId = catSplit[1];
            if (catSplit[0] == "videos")
            {
                return await GetVideos(query, cancellationToken).ConfigureAwait(false);
            }
            return null;
        }

        private async Task<ChannelItemResult> GetCategories(CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue("categories-redtube", out ChannelItemResult o))
            {
                return o;
            }
            var items = new List<ChannelItemInfo>();
            HttpClientHandler httpClientHandler = new HttpClientHandler() { CheckCertificateRevocationList = false, ClientCertificateOptions = ClientCertificateOption.Automatic };
            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string site = await new HttpClient(httpClientHandler).GetStringAsync("http://api.redtube.com/?data=redtube.Categories.getCategoriesList&output=json");

            _logger.LogInformation(site);
            var categories = jsonSerializer.DeserializeFromString<RootObject>(site);

            foreach (var c in categories.categories)
            {
                if (c.category != "japanesecensored")
                {
                    items.Add(new ChannelItemInfo
                    {
                        Name = c.category,//.Substring(0, 1).ToUpper() + c.category.Substring(1),
                        Id = "videos_" + c.category,
                        Type = ChannelItemType.Folder,
                        FolderType = ChannelFolderType.Container,

                        ImageUrl = "https://ei.rdtcdn.com/www-static/cdn_files/redtube/images/pc/category/" + c.category.ToLower().Replace(" ", "") + "_001.jpg"
                    });
                }
            }
            memoryCache.Set("categories-redtube", new ChannelItemResult
            {
                Items = items.ToList()
            }, DateTimeOffset.Now.AddDays(7));
            return new ChannelItemResult
            {
                Items = items.ToList()
            };
        }

        private async Task<ChannelItemResult> GetVideos(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue("redtube-" + query.FolderId, out ChannelItemResult o))
            {
                return o;
            }
            var items = new List<ChannelItemInfo>();
            int total = 0;
            HttpClientHandler httpClientHandler = new HttpClientHandler() { CheckCertificateRevocationList = false, ClientCertificateOptions = ClientCertificateOption.Automatic };
            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            for (int i = 1; 3 > i; i++)
            {
                string site = await new HttpClient(httpClientHandler).GetStringAsync(String.Format("http://api.redtube.com/?data=redtube.Videos.searchVideos&output=json&category={0}&ordering=newest&thumbsize=medium2&page={1}", query.FolderId.Replace("videos_", "").Replace(" ", "%20"), i.ToString()));

                var videos = jsonSerializer.DeserializeFromString<RootObject>(site);

                total = total + videos.count;

                foreach (var v in videos.videos)
                {
                    var durationNode = v.video.duration.Split(':');
                    var time = Convert.ToDouble(durationNode[0] + "." + durationNode[1]);

                    items.Add(new ChannelItemInfo
                    {
                        Type = ChannelItemType.Media,
                        ContentType = ChannelMediaContentType.Clip,
                        MediaType = ChannelMediaType.Video,
                        FolderType = ChannelFolderType.Container,

                        ImageUrl = v.video.default_thumb.Replace("m.jpg", "b.jpg"),
                        Name = v.video.title,
                        Id = v.video.video_id,
                        HomePageUrl = v.video.url,
                        RunTimeTicks = TimeSpan.FromMinutes(time).Ticks,
                        //     MediaSources = GetChannelItemMediaInfo(v.video.video_id, cancellationToken).Result.ToList(),
                        //Tags = v.video.tags == null ? new List<string>() : v.video.tags.Select(t => t.title).ToList(),
                        DateCreated = DateTime.Parse(v.video.publish_date),
                        CommunityRating = float.Parse(v.video.rating)
                    });
                }
            }
            var finish = new ChannelItemResult
            {
                Items = items.ToList(),
                TotalRecordCount = total
            };
            memoryCache.Set("redtube-" + query.FolderId, finish, DateTimeOffset.Now.AddHours(6));
            return finish;
        }

        public async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, CancellationToken cancellationToken)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                CheckCertificateRevocationList = false,
            };
            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
            var response = await _httpClientFactory.CreateClient().GetAsync("https://embed.redtube.com/?id=" + id);
            var html = response.ToString();
            html = html.Substring(html.IndexOf("mediaDefinitions"));
            html = "{\"" + html.Substring(0, html.IndexOf(",\"video_unavailable")) + "}";
            Models.MediaInfo.root root = JsonConvert.DeserializeObject<Models.MediaInfo.root>(html);
            List<MediaSourceInfo> mediaSourceInfos = new List<MediaSourceInfo>();
            foreach (var aa in root.MediaDefinitions)
            {
                mediaSourceInfos.Add(new MediaSourceInfo()
                {
                    Id = aa.VideoUrl,
                    Path = aa.VideoUrl,
                    IsRemote = true,
                    VideoType = VideoType.VideoFile,
                    EncoderProtocol = MediaProtocol.File,

                    Protocol = MediaProtocol.File,
                }); ;
            }
            return mediaSourceInfos;
        }

        public Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case
                ImageType.Primary:
                    {
                        var path = GetType().Namespace + ".Images.logo.png";

                        return Task.FromResult(new DynamicImageResponse
                        {
                            Format = ImageFormat.Png,
                            HasImage = true,

                            Stream = GetType().Assembly.GetManifestResourceStream(path)
                        });
                    }
                case
                ImageType.Thumb:
                    {
                        var path = GetType().Namespace + ".Images.logo.png";

                        return Task.FromResult(new DynamicImageResponse
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

        public IEnumerable<ImageType> GetSupportedChannelImages()
        {
            return new List<ImageType>
            {
                ImageType.Primary,
                ImageType.Thumb
            };
        }

        public async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ChannelLatestMediaSearch request, CancellationToken cancellationToken)
        {
            var tmp = await GetCategories(cancellationToken);
            int i = (int)(tmp.TotalRecordCount - 1);
            var videos = await GetVideos(new InternalChannelItemQuery() { FolderId = tmp.Items[new Random(DateTime.Now.Millisecond).Next(0, i)].Id }, cancellationToken);
            List<ChannelItemInfo> list = videos.Items;
            return videos.Items;
        }
    }
}