using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Channel.TubiTV.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Channel.TubiTV
{
    public class Worker
    {
        /// <summary>
        /// Gets the first page of the channel, this is premade with static variables because i couldnt find a dynamic way to collect this
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> GetGenres(ILogger logger)
        {
            ChannelItemResult result = new ChannelItemResult();
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "action",
                Name = "Action",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "anime",
                Name = "Anime",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "classics",
                Name = "Classics",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;

            result.Items.Add(new ChannelItemInfo()
            {
                Id = "comedy",
                Name = "Comedy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "crime_tv".ToLower(),
                Name = "Crime TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Documentary".ToLower(),
                Name = "Documentary",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Docuseries".ToLower(),
                Name = "Docuseries",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Drama".ToLower(),
                Name = "Drama",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "faith_and_spirituality".ToLower(),
                Name = "Faith",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Family_Movies".ToLower(),
                Name = "Family Movies",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "foreign_films".ToLower(),
                Name = "Foreign Language Films",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Foreign_Language_TV".ToLower(),
                Name = "Foreign Language TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Horror".ToLower(),
                Name = "Horror",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Indie_Films".ToLower(),
                Name = "Indie Films",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Kids_Shows".ToLower(),
                Name = "Kids Shows",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "LGBT".ToLower(),
                Name = "LGBTQ",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Lifestyle".ToLower(),
                Name = "Lifestyle",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Martial_Arts".ToLower(),
                Name = "Martial Arts",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "music_musicals".ToLower(),
                Name = "Music & Concerts",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Preschool".ToLower(),
                Name = "Preschool",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Reality_TV".ToLower(),
                Name = "Reality TV",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Romance".ToLower(),
                Name = "Romance",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Sci_fi_and_Fantasy".ToLower(),
                Name = "SciFi And Fantasy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Stand_Up_Comedy".ToLower(),
                Name = "Stand Up Comedy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Thrillers".ToLower(),
                Name = "Thrillers",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "TV_Comedies".ToLower(),
                Name = "TV Comedies",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "TV_Dramas".ToLower(),
                Name = "TV Dramas",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;
            result.Items.Add(new ChannelItemInfo()
            {
                Id = "Westerns".ToLower(),
                Name = "Westerns",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            result.TotalRecordCount++;

            return result;
        }

        /// <summary>
        /// This function grabs all the items that belongs to selected genre
        /// </summary>
        /// <param name="GenreId"></param>
        /// <param name="logger"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        internal async Task<ChannelItemResult> CollectGenreItemsAsync(string GenreId, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue("tubitv-categories-" + GenreId, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(CollectGenreItemsAsync), "tubitv-categories-" + GenreId);
                return cachedValue;
            }
            ChannelItemResult result = new ChannelItemResult();

            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://tubitv.com/oz/containers/" + GenreId.ToLower().Replace(" ", "_") + "/content?parentId=&limit=5000&isKidsModeEnabled=false");
            dynamic test = JObject.Parse(json);
            List<GenreItems> list = new List<GenreItems>();

            foreach (dynamic jObject in test.contents)
            {
                if (jObject == null)
                {
                    continue;
                }
                try
                {
                    string id = jObject.Value.id;
                    string type = jObject.Value.type;
                    string title = jObject.Value.title;
                    string decription = jObject.Value.description;
                    List<string> Tags = new List<string>();
                    foreach (dynamic jj in jObject.Value.tags)
                        Tags.Add(jj.Value);
                    List<string> Genres = new List<string>();
                    foreach (dynamic jj in jObject.Value.tags)
                        Genres.Add(jj.Value);
                    List<string> Directors = new List<string>();
                    foreach (dynamic director in jObject.Value.directors)
                        Directors.Add(director.Value);
                    List<string> Actors = new List<string>();
                    foreach (dynamic actor in jObject.Value.actors)
                        Actors.Add(actor.Value);
                    List<string> PosterArt = new List<string>();
                    foreach (dynamic art in jObject.Value.posterarts)
                        PosterArt.Add(art.Value);
                    List<string> thumbnails = new List<string>();
                    foreach (dynamic art in jObject.Value.thumbnails)
                        thumbnails.Add(art.Value);
                    string language = jObject.Value.lang;
                    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(decription) || string.IsNullOrEmpty(language))
                    {
                        continue;
                    }

                    GenreItems item = new GenreItems(id, type, title, decription, language, Tags, Directors, Actors, PosterArt, thumbnails);
                    list.Add(item);
                }
                catch (Exception e)
                {
                    logger.LogInformation("Error parsin value from dynamic json variable  CollectGenreItemsAsync");
                    continue;
                }
            }

            foreach (GenreItems i in list)
            {
                if (string.Equals(i.type, "v", StringComparison.OrdinalIgnoreCase))
                {
                    result.Items.Add(new ChannelItemInfo()
                    {
                        Id = i.id,
                        Genres = i.tags,
                        Name = i.title ?? "Unknown",
                        ImageUrl = i.Thumbnail.First(x => !string.IsNullOrEmpty(x)) ?? "https://upload.wikimedia.org/wikipedia/en/thumb/d/db/Tubi_logo.svg/1200px-Tubi_logo.svg.png",
                        FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                        MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                        Type = ChannelItemType.Media,
                        OriginalTitle = i.title ?? "Unknown",
                        Overview = i.description ?? "Unknown",
                    });
                    result.TotalRecordCount++;
                }
                else if (string.Equals(i.type, "s", StringComparison.OrdinalIgnoreCase))
                {
                    result.Items.Add(new ChannelItemInfo()
                    {
                        Id = "series-0" + i.id,
                        Genres = i.tags,
                        Name = i.title ?? "Unknown",
                        SeriesName = i.title,
                        ImageUrl = i.Thumbnail.First(x => !string.IsNullOrEmpty(x)) ?? "https://upload.wikimedia.org/wikipedia/en/thumb/d/db/Tubi_logo.svg/1200px-Tubi_logo.svg.png",
                        FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                        MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                        Type = ChannelItemType.Folder,
                        OriginalTitle = i.title ?? "Unknown",
                        Overview = i.description ?? "Unknown",
                    });
                    result.TotalRecordCount++;
                }
            }
            memoryCache.Set("tubitv-categories-" + GenreId, result, DateTimeOffset.Now.AddDays(1));
            return result;
        }

        internal async Task<ChannelItemResult> CollecEpisodeItemsAsync(string GenreId, ILogger logger, IMemoryCache memoryCache)
        {
            string[] tmp = GenreId.Split("-");
            string id = tmp.Last();
            if (memoryCache.TryGetValue("tubitv-episodes-" + id, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(CollecEpisodeItemsAsync), "tubitv-episodes-" + id);
                return cachedValue;
            }
            ChannelItemResult result = new ChannelItemResult();

            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://tubitv.com/oz/videos/" + id + "/content");
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<SeasonEpisodeInfo.root>(json);

            List<GenreItems> list = new List<GenreItems>();
            string img = string.Empty;
            foreach (var jObject in root.Children)
            {
                if (jObject == null)
                {
                    continue;
                }
                if (jObject.Id == tmp[1])
                {
                    foreach (var uuu in jObject.Children)
                    {
                        result.Items.Add(new ChannelItemInfo()
                        {
                            Id = uuu.Id,
                            SeriesName = jObject.Title,
                            Name = uuu.Title,
                            Overview = uuu.Description,

                            FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                            MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                            ImageUrl = uuu.Thumbnails.First(x => !string.IsNullOrEmpty(x)),
                            Genres = uuu.Tags.ToList(),
                            ProductionYear = uuu.Year,
                            Type = ChannelItemType.Media,
                            OriginalTitle = uuu.Title ?? "Unknown",
                        });

                        result.TotalRecordCount++;
                    }
                }
                else
                {
                }
            }
            memoryCache.Set("tubitv-episodes-" + id, result, DateTimeOffset.Now.AddDays(2));
            return null;
        }

        internal async Task<ChannelItemResult> CollecSeasonItemsAsync(string GenreId, ILogger logger, IMemoryCache memoryCache)
        {
            string id = GenreId.Replace("series-", "");
            if (memoryCache.TryGetValue("tubitv-season-" + id, out ChannelItemResult cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(CollectGenreItemsAsync), "tubitv-season-" + id);
                return cachedValue;
            }
            ChannelItemResult result = new ChannelItemResult();

            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://tubitv.com/oz/videos/" + id + "/content");
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<SeasonEpisodeInfo.root>(json);

            List<GenreItems> list = new List<GenreItems>();
            string img = string.Empty;
            foreach (var jObject in root.Children)
            {
                if (jObject == null)
                {
                    continue;
                }
                string idd = jObject.Id;
                string title = jObject.Title;
                logger.LogInformation(idd);
                result.Items.Add(new ChannelItemInfo()
                {
                    Id = "season-" + idd + "-" + id,
                    SeriesName = title ?? "Unknown",
                    Name = title ?? "Unknown",
                    FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                    MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                    ImageUrl = root.Thumbnails.First(),
                    Type = ChannelItemType.Folder,
                    OriginalTitle = title ?? "Unknown",
                });

                result.TotalRecordCount++;
            }
            memoryCache.Set("tubitv-season-" + id, result, DateTimeOffset.Now.AddDays(2));
            return result;
        }

        internal async Task<IEnumerable<ChannelItemInfo>> CollectLatestadded(ILogger logger, IMemoryCache memoryCache)
        {
            logger.LogInformation("latestmedi1");
            if (memoryCache.TryGetValue("tubitv-latest", out IEnumerable<ChannelItemInfo> cachedValue))
            {
                logger.LogInformation("Function={function} FolderId={folderId} Cache Hit", nameof(CollectGenreItemsAsync), "tubitv-latest");
                return cachedValue;
            }
            try
            {
                HttpClient httpClient = new HttpClient();
                List<ChannelItemInfo> result = new List<ChannelItemInfo>();
                string json = await httpClient.GetStringAsync("https://tubitv.com/oz/containers/recently_added/content?parentId=&cursor=10&limit=60&isKidsModeEnabled=false");
                dynamic test = JObject.Parse(json);
                foreach (dynamic jObject in test.contents)
                {
                    try
                    {
                        string id = jObject.Value.id;
                        string title = jObject.Value.title;
                        string decription = jObject.Value.description;

                        List<string> PosterArt = new List<string>();
                        foreach (dynamic art in jObject.Value.thumbnails)
                            PosterArt.Add(art.Value);
                        result.Add(new ChannelItemInfo()
                        {
                            Id = id,
                            Name = title,
                            // ImageUrl = PosterArt.First(x => !string.IsNullOrEmpty(x)),
                            // Overview = decription,
                            MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video,
                            Type = ChannelItemType.Media,
                        });
                    }
                    catch (Exception e)
                    {
                    }
                }
                logger.LogInformation(result.Count.ToString());
                logger.LogInformation("latestmedi ferdig");
                memoryCache.Set("tubitv-latest", result, DateTimeOffset.Now.AddDays(1));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error parsin json in CollectGenreItemsAsync");
                logger.LogError(ex.Message);
                return new List<ChannelItemInfo>() { new ChannelItemInfo() };
            }
        }

        internal async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogInformation("Grabbing stream data for " + id);
            HttpClient httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://tubitv.com/oz/videos/" + id + "/content");
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaybackInfo.root>(json);
            return new List<MediaSourceInfo>()
            {
                 new MediaSourceInfo()
                 {
                     Path = root.VideoResources.First().Manifest.Url,
                     Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     Id = root.Id,
                     Name = root.Title,

                     Type = MediaSourceType.Default,
                     IsRemote = true,
                     EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     VideoType = MediaBrowser.Model.Entities.VideoType.VideoFile
                 },
            };
        }
    }
}