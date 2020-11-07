using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Channel.Pornhub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Pornhub
{
    public class Channel : IChannel, IRequiresMediaInfoCallback, ISupportsLatestMedia
    {
        public string Name => Plugin.Instance.Name;

        public string Description => Plugin.Instance.Description;

        public string DataVersion => "1.0.0.0";

        public string HomePageUrl => "";

        private readonly ILogger<Channel> logger;
        private readonly IMemoryCache memoryCache;
        public ChannelParentalRating ParentalRating => ChannelParentalRating.Adult;

        public Channel(ILogger<Channel> logger, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
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
                MaxPageSize = 20
            };
        }

        public Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case ImageType.Primary:
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
                ImageType.Primary
            };
        }

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.FolderId))
                return await GetCategories();

            return await GetVideos(query.FolderId);
        }

        private async Task<ChannelItemResult> GetCategories()
        {
            if (memoryCache.TryGetValue("pornhub-categories", out ChannelItemResult o))
            {
                return o;
            }
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://www.pornhub.com/webmasters/categories");
            var results = JsonConvert.DeserializeObject<CategoryResults.root>(json);
            ChannelItemResult result = new ChannelItemResult();
            foreach (var category in results.Categories)
            {
                result.Items.Add(new ChannelItemInfo()
                {
                    Id = category.Id,
                    Name = category.category,
                    Type = ChannelItemType.Folder,
                });
                result.TotalRecordCount++;
            }
            memoryCache.Set("pornhub-categories", result, DateTimeOffset.Now.AddDays(7));
            return result;
        }

        private async Task<ChannelItemResult> GetVideos(string CategoryName)
        {
            if (memoryCache.TryGetValue("pornhub-" + CategoryName, out ChannelItemResult o))
            {
                return o;
            }
            HttpClient httpClient = new HttpClient();
            ChannelItemResult result = new ChannelItemResult();
            for (int i = 1; 3 >= i; i++)
            {
                string json = new WebClient().DownloadString("https://www.pornhub.com/webmasters/search?q=&category=" + CategoryName + "&ordering=newest&thumbsize=large&page=" + i.ToString());
                var results = JsonConvert.DeserializeObject<SearchResult.root>(json);
                foreach (var video in results.Videos)
                {
                    result.Items.Add(new ChannelItemInfo()
                    {
                        CommunityRating = float.Parse(video.Rating.ToString()),
                        DateCreated = Convert.ToDateTime(video.PublishDate),
                        ContentType = ChannelMediaContentType.Clip,
                        Name = video.Title,
                        ImageUrl = video.DefaultThumb,
                        FolderType = ChannelFolderType.Container,
                        Type = ChannelItemType.Media,
                        MediaType = ChannelMediaType.Video,
                        HomePageUrl = video.Url,
                        OfficialRating = video.Ratings.ToString(),
                        OriginalTitle = video.Title,
                        Id = video.VideoId
                    });
                    result.TotalRecordCount++;
                }
            }

            memoryCache.Set("pornhub-" + CategoryName, result, DateTimeOffset.Now.AddHours(6));

            return result;
        }

        public bool IsEnabledFor(string userId)
        {
            return true;
        }

        public async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, CancellationToken cancellationToken)
        {
            string source = new WebClient().DownloadString("https://www.pornhub.com/embed/" + id);
            source = source.Substring(source.IndexOf("document.referrer.split('/')[2]") + "document.referrer.split('/')[2]".Length);
            source = source.Substring(0, source.IndexOf("if (utmSource == ''"));

            string[] variables = source.Split(new[] { "var" }, StringSplitOptions.None);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string ferdig = string.Empty;
            List<string> StreamURL = new List<string>();
            foreach (string variable in variables)
            {
                if (variable.Contains("mediaDefinitions"))
                {
                    if (string.IsNullOrEmpty(ferdig))
                        continue;
                    if (ferdig.Contains(".mp4?validfrom=1603404"))
                        continue;
                    logger.LogInformation(ferdig);
                    StreamURL.Add(ferdig);
                    ferdig = string.Empty;
                    continue;
                }
                if (variable.StartsWith("mp4") || variable.StartsWith("hls") || variable.StartsWith(" mp4") || variable.StartsWith(" hls"))
                {
                    string cleanup = variable;
                    MatchCollection matches = Regex.Matches(cleanup, @"(?<name>\/\* \+ .*? \+ \*\/)");
                    foreach (Match match in matches)
                    {
                        cleanup = cleanup.Replace(match.Value, "");
                    }
                    cleanup = cleanup.Replace("mp4480p=", "");
                    cleanup = cleanup.Replace("hls480p=", "");
                    cleanup = cleanup.Replace("hls480=", "");
                    cleanup = cleanup.Replace("mp4480p", "");
                    cleanup = cleanup.Replace("hls480p", "");
                    cleanup = cleanup.Replace("hls480", "");
                    cleanup = cleanup.Replace(";flash", "");

                    foreach (string t in cleanup.Split("+"))
                    {
                        if (t.Contains("mediaDefinitions"))
                        {
                            StreamURL.Add(ferdig);
                            ferdig = string.Empty;
                            continue;
                        }
                        string clean = t;
                        while (clean.Contains(" "))
                            clean = clean.Replace(" ", "");
                        while (clean.Contains(";"))
                            clean = clean.Replace(";", "");
                        if (keyValuePairs.ContainsKey(clean))
                        {
                            ferdig = ferdig + keyValuePairs[clean];
                        }
                    }
                }
                if (!variable.Contains("="))
                    continue;
                string first = variable.Substring(0, variable.IndexOf("="));
                string sec = variable.Substring((variable.IndexOf("=") + 1), variable.Length - (variable.IndexOf("=") + 1));
                keyValuePairs.Add(first.Replace(" ", ""), sec.Replace(" ", "").Replace("\" + \"", "").Replace("\"", "").Replace(";", "").Replace("+", ""));
            }
            List<MediaSourceInfo> resu = new List<MediaSourceInfo>();
            foreach (string uri in StreamURL)
            {
                resu.Add(new MediaSourceInfo()
                {
                    Id = uri,
                    Path = uri,
                    IsRemote = true,
                    VideoType = VideoType.VideoFile,
                    EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                    Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                });
            }
            return resu;
        }

        public async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ChannelLatestMediaSearch request, CancellationToken cancellationToken)
        {
            var tmp = await GetCategories();
            int i = (int)(tmp.TotalRecordCount - 1);
            var videos = await GetVideos(tmp.Items[new Random(DateTime.Now.Millisecond).Next(0, i)].Name);
            List<ChannelItemInfo> list = videos.Items;
            return videos.Items;
        }
    }
}