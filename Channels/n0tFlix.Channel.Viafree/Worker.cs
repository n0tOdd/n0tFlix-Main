using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Channel.Viafree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;

namespace n0tFlix.Channel.Viafree
{
    public class Worker
    {
        public readonly string CountryCode = string.Empty;
        internal readonly string ImageSize = "800x450";

        public Worker()
        {
            this.CountryCode = GetRegionString().Result;
        }

        /// <summary>
        /// Used to grab your country code from the api
        /// </summary>
        /// <returns></returns>
        internal async Task<string> GetRegionString()
        {
            RegionInfoResults.root regionInfo = await RegionInfoResults.GetRoot();
            return regionInfo.CountryCode.ToLower();
        }

        public Dictionary<string, List<string>> CategoryShows = new Dictionary<string, List<string>>();

        /// <summary>
        /// This grabs channels and categories awailable on viafree
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetFirstPage(ILogger logger, IMemoryCache memoryCache)
        {
            var rr = await AllprogramsResults.GetRoot(default, CountryCode, default);
            ChannelItemResult channelItemResult = new ChannelItemResult();
            foreach (var category in rr._embedded.viafreeBlocks.First().allPrograms.categories)
            {
                logger.LogInformation(category.name);
                var info = await CategoriesResult.GetRoot(true, CountryCode, category.guid);
                logger.LogInformation(info.Embedded.Categories.Name);
                if (info.Embedded.Categories.Images.Image == null)
                {
                    channelItemResult.Items.Add(new ChannelItemInfo()
                    {
                        Id = "category_" + category.guid + "_" + category.name,
                        Name = category.name,
                        //  ImageUrl = info.Embedded.Categories.Images.Image.Href.Replace("{size}", ImageSize),
                        Type = ChannelItemType.Folder,
                        ContentType = ChannelMediaContentType.Episode,
                        FolderType = ChannelFolderType.Container
                    });
                }
                else
                {
                    channelItemResult.Items.Add(new ChannelItemInfo()
                    {
                        Id = "category_" + category.guid + "_" + info.Embedded.Categories.Slug,
                        Name = info.Embedded.Categories.Name,
                        ImageUrl = info.Embedded.Categories.Images.Image.Href.Replace("{size}", ImageSize),
                        Type = ChannelItemType.Folder,
                        ContentType = ChannelMediaContentType.Episode,
                        FolderType = ChannelFolderType.Container
                    });
                }
                channelItemResult.TotalRecordCount++;
            }

            return channelItemResult;
        }

        internal async Task<ChannelItemResult> GetItemsFromThisSection(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue(query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderid} Cache Hit", nameof(GetItemsFromThisSection), query.FolderId);
                return cachedValue;
            }
            else
            {
                /*              string id = query.FolderId.Split("_")[1];
                              var all = await AllprogramsResults.GetRoot(default, CountryCode, default);
                              ChannelItemResult channelItemResulta = new ChannelItemResult();
                              foreach (var program in all._embedded.viafreeBlocks.First()._embedded.programs)
                              {
                                  if (program.categories.First().guid.Equals(id, StringComparison.OrdinalIgnoreCase))
                                  {
                                      if (program.type != "series")
                                      {
                                          logger.LogDebug("This is not series, what is " + program.type + " with name " + program.title);
                                          continue;
                                      }
                                      var seriesResult = await SeriesResult.GetRoot(logger, false, CountryCode, program.slug);
                                      channelItemResulta.Items.Add(new ChannelItemInfo()
                                      {
                                          Name = program.title,
                                          Id = "program_" + program.guid ?? program.publicPath,
                                          ImageUrl = seriesResult.Meta.Image,
                                          SeriesName = program.title,
                                          Overview = seriesResult.Meta.Description,
                                          Type = ChannelItemType.Folder,
                                          FolderType = ChannelFolderType.Container,
                                          ContentType = ChannelMediaContentType.Episode,
                                          HomePageUrl = seriesResult.Meta.CanonicalUrl,
                                      });
                                      channelItemResulta.TotalRecordCount++;
                                  }
                              }
                              return channelItemResulta;
              */
                string q = query.FolderId.Split("_").Last();
                bool PublicPath = false;
                if (q.Contains("sport") || q.Contains("kanal") || q.Contains("serie") || q.Contains("program"))
                    PublicPath = true;
                mediafeed_series_by_X_Results.root root = null;
                if (query.FolderId.StartsWith("channel"))
                    root = await mediafeed_series_by_X_Results.GetRootAsChannel(PublicPath, CountryCode, q);
                else
                    root = await mediafeed_series_by_X_Results.GetRootAsCategory(PublicPath, CountryCode, q);
                if (root == null)
                {
                    logger.LogError("Function={function} FolderId={folderid} We couldnt get the root part of this json", nameof(GetItemsFromThisSection), query.FolderId);
                    return default;
                }
                ChannelItemResult channelItemResult = new ChannelItemResult();
                foreach (var program in root.Embedded.Programs)
                {
                    if (program.Type != "series")
                    {
                        logger.LogDebug("This is not series, what is " + program.Type + " with name " + program.Title);
                        continue;
                    }
                    channelItemResult.Items.Add(new ChannelItemInfo()
                    {
                        Name = program.Title,
                        Overview = program.Synopsis.Long ?? program.Synopsis.Short ?? "Unknown",
                        Id = "program_" + program.Guid ?? program.PublicPath,
                        ImageUrl = program.Images.Landscape.Href.Replace("{size}", ImageSize) ?? program.Images.PlayerPoster.Href.Replace("{size}", ImageSize) ?? program.Images.Boxart.Href.Replace("{size}", ImageSize) ?? "https://mpng.subpng.com/20171216/849/pirate-png-5a350658adbbb2.2125216315134244727116.jpg",
                        Type = ChannelItemType.Folder,
                        // todo add runtimeticks?
                        ContentType = ChannelMediaContentType.Episode,
                        OriginalTitle = program.CombinedTitle,
                        FolderType = ChannelFolderType.Container,
                        HomePageUrl = program.Links.Self.Href ?? program.Links.Series.Href
                    });
                    channelItemResult.TotalRecordCount++;
                }
                memoryCache.Set(query.FolderId, channelItemResult, DateTimeOffset.Now.AddDays(1));
                return channelItemResult;
            }
        }

        internal async Task<ChannelItemResult> GetSeasonsAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue(query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderid} Cache Hit", nameof(GetSeasonsAsync), query.FolderId);
                return cachedValue;
            }
            else
            {
                string guid = query.FolderId.Split("_").Last();
                bool PublicPath = false;
                if (guid.Contains("/programmer") || guid.Contains("series") || guid.Contains("sport") || guid.Contains("kanal"))
                    PublicPath = true;
                var results = await SeriesResult.GetRoot(logger, PublicPath, CountryCode, guid);
                ChannelItemResult channelItemResult = new ChannelItemResult();
                foreach (var block in results.Embedded.ViafreeBlocks[2].Embedded.Seasons)
                {
                    channelItemResult.Items.Add(new ChannelItemInfo()
                    {
                        Name = block.Title,
                        Id = "season_" + guid,
                        FolderType = ChannelFolderType.Container,
                        Type = ChannelItemType.Folder,
                        ImageUrl = results.Embedded.ViafreeBlocks[0].Links.Image.Href.Replace("{size}", ImageSize) ?? results.Meta.Image.Replace("{size}", ImageSize) ?? "https://mpng.subpng.com/20171216/849/pirate-png-5a350658adbbb2.2125216315134244727116.jpg",
                        Overview = results.Meta.Description,
                        ContentType = ChannelMediaContentType.Episode,

                        MediaType = ChannelMediaType.Video,
                        HomePageUrl = block.Links.Season.Href,
                    });
                    channelItemResult.TotalRecordCount++;
                }
                memoryCache.Set(query.FolderId, channelItemResult, DateTimeOffset.Now.AddDays(1));
                return channelItemResult;
            }
        }

        internal async Task<ChannelItemResult> GetEpisodesAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue(query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderid} Cache Hit", nameof(GetSeasonsAsync), query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogInformation("Function={function} FolderId={folderid} Web download", nameof(GetSeasonsAsync), query.FolderId);
                string guid = query.FolderId.Split("_").Last();
                bool PublicPath = false;
                if (guid.Contains("/programmer") || guid.Contains("series") || guid.Contains("sport") || guid.Contains("kanal"))
                    PublicPath = true;
                var results = await SeriesResult.GetRoot(logger, PublicPath, CountryCode, guid);
                ChannelItemResult channelItemResult = new ChannelItemResult();
                foreach (var block in results.Embedded.ViafreeBlocks[3].Embedded.Blocks)
                {
                    foreach (var program in block.Embedded.Programs)
                    {
                        if (program.Type == "series" || program.Type == "episode")
                        {
                            var stream = await StreamResults.GetRoot(default, CountryCode, program.Links.StreamLink.Href);
                            List<MediaSourceInfo> mediaSources = new List<MediaSourceInfo>();
                            foreach (var video in stream.embedded.prioritizedStreams)
                            {
                                List<MediaStream> subs = new List<MediaStream>();
                                foreach (var sub in stream.embedded.subtitles)
                                    subs.Add(new MediaStream()
                                    {
                                        Type = MediaStreamType.Subtitle,
                                        IsExternal = true,
                                        IsExternalUrl = true,
                                        Path = sub.link.href,
                                        SupportsExternalStream = true,
                                        Language = sub.data.language,
                                        Title = program.Title + "_" + sub.data.language,
                                    });
                                mediaSources.Add(new MediaSourceInfo()
                                {
                                    Path = video.links.stream.href,
                                    Name = program.Title,
                                    Id = program.Video.MediaGuid,
                                    IsRemote = true,
                                    Type = MediaSourceType.Default,
                                    VideoType = VideoType.VideoFile,
                                    SupportsDirectPlay = true,
                                    SupportsDirectStream = true,
                                    MediaStreams = subs,
                                    //    RunTimeTicks = program.Video.Duration.Milliseconds,
                                    Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                                    EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                                    SupportsTranscoding = true,
                                });
                            }
                            channelItemResult.Items.Add(new ChannelItemInfo()
                            {
                                Name = program.Title,
                                Id = program.Guid,
                                ContentType = ChannelMediaContentType.Episode,
                                Type = ChannelItemType.Media,
                                MediaSources = mediaSources,
                                FolderType = ChannelFolderType.Container,
                                Overview = program.Synopsis.Long ?? program.Synopsis.Short,
                                RunTimeTicks = program.Video.Duration.Milliseconds,
                                ImageUrl = program.Images.Landscape.Href.Replace("{size}", ImageSize) ?? program.Images.PlayerPoster.Href.Replace("{size}", ImageSize) ?? program.Images.SeasonImage.Href.Replace("{size}", ImageSize) ?? "https://mpng.subpng.com/20171216/849/pirate-png-5a350658adbbb2.2125216315134244727116.jpg",
                                MediaType = ChannelMediaType.Video,

                                HomePageUrl = program.Links.Play.Href,
                                SeriesName = program.Episode.SeriesTitle,
                            });
                            channelItemResult.TotalRecordCount++;
                        }
                        else
                        {
                            logger.LogInformation("We found a new type, add a class for " + program.Type + " its named " + program.Title);
                        }
                    }
                }
                memoryCache.Set(query.FolderId, channelItemResult, DateTimeOffset.Now.AddDays(1));
                return channelItemResult;
            }
        }

        internal async Task<IEnumerable<ChannelItemInfo>> GetLatestMedia(ILogger logger, ChannelLatestMediaSearch request, CancellationToken cancellationToken)
        {
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");
            logger.LogInformation("start");

            logger.LogInformation("start");
            //Todo get the latest contect awailable to show on the start page
            List<ChannelItemInfo> channelItemInfos = new List<ChannelItemInfo>();
            var root = await mediaFeed_latest_episodes_Results.GetRoot(default, CountryCode, default);
            logger.LogInformation("Found new episodes");
            foreach (var program in root.Embedded.Programs)
            {
                if (channelItemInfos.Count >= 5)
                    break;
                if (program.Type == "episode")
                {
                    logger.LogInformation("found it " + program.Type + " its named " + program.Title);
                    try
                    {
                        var stream = await StreamResults.GetRoot(default, CountryCode, program.Links.StreamLink.Href);
                        List<MediaSourceInfo> mediaSources = new List<MediaSourceInfo>();
                        List<MediaStream> subs = new List<MediaStream>();
                        foreach (var sub in stream.embedded.subtitles)
                            subs.Add(new MediaStream()
                            {
                                Type = MediaStreamType.Subtitle,
                                IsExternal = true,
                                IsExternalUrl = true,
                                Path = sub.link.href,
                                SupportsExternalStream = true,
                                Language = sub.data.language,
                                Title = program.Title + "_" + sub.data.language,
                            });

                        if (subs.Count != 0)
                        {
                            foreach (var video in stream.embedded.prioritizedStreams)
                            {
                                mediaSources.Add(new MediaSourceInfo()
                                {
                                    Path = video.links.stream.href,
                                    Name = program.Title,
                                    Id = program.Video.MediaGuid,
                                    IsRemote = true,
                                    Type = MediaSourceType.Default,
                                    VideoType = VideoType.VideoFile,
                                    SupportsDirectPlay = true,
                                    SupportsDirectStream = true,
                                    MediaStreams = subs,
                                    //    RunTimeTicks = program.Video.Duration.Milliseconds,
                                    Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                                    EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.Http,
                                    SupportsTranscoding = true,
                                });
                            }
                            channelItemInfos.Add(new ChannelItemInfo()
                            {
                                Name = program.Title,
                                Id = program.Guid,
                                ContentType = ChannelMediaContentType.Episode,
                                Type = ChannelItemType.Media,
                                MediaSources = mediaSources,
                                FolderType = ChannelFolderType.Container,
                                // Overview = program.Synopsis.Long ?? program.Synopsis.Short,
                                //RunTimeTicks = program.Video.Duration.Milliseconds,
                                //    ImageUrl = program.Images.Landscape.Href.Replace("{size}", ImageSize) ?? program.Images.PlayerPoster.Href.Replace("{size}", ImageSize) ?? program.Images.SeasonImage.Href.Replace("{size}", ImageSize),
                                MediaType = ChannelMediaType.Video,

                                //           HomePageUrl = program.Links.Play.Href,
                                //         SeriesName = program.Episode.SeriesTitle,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogInformation(ex.Message);
                    }
                }
                else
                {
                    logger.LogInformation("We found a new type, add a class for " + program.Type + " its named " + program.Title);
                }
            }
            return channelItemInfos;
        }
    }
}