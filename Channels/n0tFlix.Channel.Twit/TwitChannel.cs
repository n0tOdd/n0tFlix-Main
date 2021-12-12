using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Controller.Drawing;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using MediaBrowser.Model.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Drawing;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace n0tFlix.Channel.TWiT
{
    public class TwitChannel : IChannel, IRequiresMediaInfoCallback
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TwitChannel> _logger;
        private readonly IXmlSerializer _xmlSerializer;

        public TwitChannel(IHttpClientFactory httpClientFactory, IXmlSerializer xmlSerializer, ILogger<TwitChannel> logManager)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logManager;
            _xmlSerializer = xmlSerializer;
        }

        // IChannel Interface Implementation
        public string DataVersion
        {
            get { return "5"; }
        }

        public string Description
        {
            get { return "The TWiT.tv Netcast Network with Leo Laporte features the #1 ranked technology podcast This Week in Tech, along with over 20 other top-ranked online shows."; }
        }

        public string HomePageUrl
        {
            get { return "http://twit.tv"; }
        }

        public string Name
        {
            get { return "TWiT"; }
        }

        public ChannelParentalRating ParentalRating
        {
            get { return ChannelParentalRating.GeneralAudience; }
        }

        public InternalChannelFeatures GetChannelFeatures()
        {
            return new InternalChannelFeatures
            {
                ContentTypes = new List<ChannelMediaContentType>
                {
                    ChannelMediaContentType.Podcast,
                },

                MediaTypes = new List<ChannelMediaType>
                {
                    ChannelMediaType.Video
                },

                MaxPageSize = 100,

                DefaultSortFields = new List<ChannelItemSortField>
                {
                    ChannelItemSortField.Name,
                    ChannelItemSortField.PremiereDate,
                    ChannelItemSortField.Runtime,
                },
            };
        }

        public Task<DynamicImageResponse> GetChannelImage(ImageType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case ImageType.Thumb:
                case ImageType.Backdrop:
                case ImageType.Primary:
                    {
                        var path = GetType().Namespace + ".Images." + type.ToString().ToLower() + ".jpg";

                        return Task.FromResult(new DynamicImageResponse
                        {
                            Format = ImageFormat.Jpg,
                            HasImage = true,
                            Stream = GetType().Assembly.GetManifestResourceStream(path)
                        });
                    }
                default:
                    throw new ArgumentException("Unsupported image type: " + type);
            }
        }

        public async Task<ChannelItemResult> GetChannelItems(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            ChannelItemResult result;

            _logger.LogDebug("cat ID : " + query.FolderId);

            if (query.FolderId == null)
            {
                result = await GetChannelsInternal(query, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                result = await GetChannelItemsInternal(query, cancellationToken).ConfigureAwait(false);
            }

            return result;
        }

        private async Task<ChannelItemResult> GetChannelsInternal(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            var twitChannels = new List<ChannelItemInfo>();

            var masterChannelList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("arena", "Android App Arena"),
                new KeyValuePair<string, string>("code", "Coding 101"),
                new KeyValuePair<string, string>("floss", "FLOSS Weekly"),
                new KeyValuePair<string, string>("dgw", "The Giz Wiz"),
                new KeyValuePair<string, string>("hn", "Ham Nation"),
                new KeyValuePair<string, string>("htg", "Home Theater Geeks"),
                new KeyValuePair<string, string>("ifive", "iFive For The iPhone"),
                new KeyValuePair<string, string>("ipad", "iPad Today"),
                new KeyValuePair<string, string>("kh", "Know How"),
                new KeyValuePair<string, string>("mbw", "MacBreak Weekly"),
                new KeyValuePair<string, string>("mm", "Marketing Mavericks"),
                new KeyValuePair<string, string>("omgcraft", "OMGcraft"),
                new KeyValuePair<string, string>("sn", "Security Now"),
                new KeyValuePair<string, string>("natn", "The Social Hour"),
                new KeyValuePair<string, string>("ttg", "The Tech Guy"),
                new KeyValuePair<string, string>("tnt", "Tech News Today"),
                new KeyValuePair<string, string>("tn2n", "Tech News 2Night"),
                new KeyValuePair<string, string>("twich", "This Week in Computer Hardware"),
                new KeyValuePair<string, string>("twich", "This Week in Computer Hardware"),

                new KeyValuePair<string, string>("twiet", "This Week in Enterprise Tech"),
                new KeyValuePair<string, string>("twig", "This Week in Google"),
                new KeyValuePair<string, string>("twil", "This Week in Law"),
                new KeyValuePair<string, string>("twit", "This Week in Tech"),
                new KeyValuePair<string, string>("tri", "Triangulation"),
                new KeyValuePair<string, string>("specials", "TWiT Live Specials"),
                new KeyValuePair<string, string>("ww", "Windows Weekly")
            };

            var altArtValues = new Dictionary<string, string>();
            altArtValues["natn"] = "tsh";

            /*int filterLimit;
            if (query.StartIndex + query.Limit <= masterChannelList.Count)
            {
                filterLimit = (int)(query.StartIndex + query.Limit) - 1;
            }
            else
            {
                filterLimit = masterChannelList.Count - 1;
            }

            var filteredChannelList = new Dictionary<string, string>();
            if (query.StartIndex != null)
            {
                for (var i = (int)query.StartIndex; i <= filterLimit; i++)
                {
                    filteredChannelList[masterChannelList[i].Key] = masterChannelList[i].Value;
                }
            }*/

            foreach (var currentChannel in masterChannelList)
            {
                if (altArtValues.ContainsKey(currentChannel.Key))
                {
                    twitChannels.Add(new ChannelItemInfo
                    {
                        Type = ChannelItemType.Folder,
                        ImageUrl = "http://feeds.twit.tv/coverart/" + altArtValues[currentChannel.Key] + "600.jpg",
                        Name = currentChannel.Value,
                        Id = altArtValues[currentChannel.Key],
                    });
                }
                else
                {
                    twitChannels.Add(new ChannelItemInfo
                    {
                        Type = ChannelItemType.Folder,
                        ImageUrl = "http://feeds.twit.tv/coverart/" + currentChannel.Key + "600.jpg",
                        Name = currentChannel.Value,
                        Id = currentChannel.Key,
                    });
                }
            }

            return new ChannelItemResult
            {
                Items = twitChannels.ToList(),
                TotalRecordCount = masterChannelList.Count,
            };
        }

        private async Task<ChannelItemResult> GetChannelItemsInternal(InternalChannelItemQuery query, CancellationToken cancellationToken)
        {
            var offset = query.StartIndex.GetValueOrDefault();
            var downloader = new TwitChannelItemsDownloader(_logger, _xmlSerializer, _httpClientFactory);

            var baseurl = "http://feeds.twit.tv/" + query.FolderId + "_video_hd.xml";

            var videos = await downloader.GetStreamList(baseurl, offset, cancellationToken)
                .ConfigureAwait(false);
            _logger.LogInformation(videos.channel.item.Length.ToString());
            var itemslist = videos.channel.item;

            var items = new List<ChannelItemInfo>();

            _logger.LogDebug("Twit found {0} items under folder {1}", itemslist.Length, query.FolderId);

            foreach (var i in itemslist)
            {
                _logger.LogInformation(i.content.url);

                var mediaInfo = new List<MediaSourceInfo>
                {
                new ChannelMediaInfo
                {
                    Protocol = MediaProtocol.Http,
                    Path = i.content.url,
                    Width = 1280,
                    Height = 720,
                }.ToMediaSource()
                };

                var runtimeArray = i.duration.Split(':');
                int hours;
                int minutes;
                int.TryParse(runtimeArray[0], out hours);
                int.TryParse(runtimeArray[1], out minutes);
                long runtime = (hours * 60) + minutes;
                runtime = TimeSpan.FromMinutes(runtime).Ticks;

                items.Add(new ChannelItemInfo
                {
                    ContentType = ChannelMediaContentType.Podcast,
                    ImageUrl = "http://feeds.twit.tv/coverart/" + query.FolderId + "600.jpg",
                    MediaType = ChannelMediaType.Video,
                    //  MediaSources = mediaInfo,
                    RunTimeTicks = runtime,
                    Name = i.title,
                    Id = i.content.url,
                    Type = ChannelItemType.Media,
                    //    DateCreated = !String.IsNullOrEmpty(i.pubDate) ?
                    //      Convert.ToDateTime(i.pubDate) : (DateTime?)null,
                    //  PremiereDate = !String.IsNullOrEmpty(i.pubDate) ?
                    //    Convert.ToDateTime(i.pubDate) : (DateTime?)null,
                    Overview = i.summary,
                });
            }

            return new ChannelItemResult
            {
                Items = items,
                TotalRecordCount = items.Count,
            };
        }

        public IEnumerable<ImageType> GetSupportedChannelImages()
        {
            return new List<ImageType>
            {
                ImageType.Thumb,
                ImageType.Primary,
                ImageType.Backdrop
            };
        }

        public bool IsEnabledFor(string userId)
        {
            return true;
        }

        //IRequiresMediaInfoCallback Interface Implementation
        public async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, CancellationToken cancellationToken)
        {
            var filenameparts = id.Split('_');
            var baseurl = filenameparts[0];

            _logger.LogDebug("**** TWiT PLAYBACK EPISODEID: " + baseurl + " ****");
            _logger.LogDebug("**** TWiT PLAYBACK HD Stream: " + baseurl + "_h264m_1280x720_1872.mp4" + " ****");
            _logger.LogDebug("**** TWiT PLAYBACK HQ Stream: " + baseurl + "_h264m_864x480_500.mp4" + " ****");
            _logger.LogDebug("**** TWiT PLAYBACK LQ Stream: " + baseurl + "_h264b_640x368_256.mp4" + " ****");

            return new List<MediaSourceInfo>
            {
                new ChannelMediaInfo
                {
                    Path = baseurl + "_h264m_1280x720_1872.mp4",
                    Width = 1280,
                    Height = 720,
                }.ToMediaSource(),
                new ChannelMediaInfo
                {
                    Path = baseurl + "_h264m_864x480_500.mp4",
                    Width = 864,
                    Height = 480,
                }.ToMediaSource(),
                new ChannelMediaInfo
                {
                    Path = baseurl  + "_h264b_640x368_256.mp4",
                    Width = 640,
                    Height = 368,
                }.ToMediaSource()
            };
        }
    }
}