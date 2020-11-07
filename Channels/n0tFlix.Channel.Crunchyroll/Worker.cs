using AngleSharp;
using AngleSharp.Dom;
using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Crunchyroll
{
    public class Worker
    {
        public Worker()
        {
        }

        public async Task<ChannelItemResult> GetCategories(ILogger logger)
        {
            ChannelItemResult channelItemResult = new ChannelItemResult();
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&action",
                Name = "Action",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&adventure",
                Name = "Adventure",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&comedy",
                Name = "Comedy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&drama",
                Name = "Drama",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&fantasy",
                Name = "Fantasy",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&harem",
                Name = "Harem",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&historical",
                Name = "Historical",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&idols",
                Name = "Idols",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&isekai",
                Name = "Isekai",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&magical_girls",
                Name = "Magical Girls",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&mecha",
                Name = "Mecha",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&music",
                Name = "Music",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&mystery",
                Name = "Mystery",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&post-apocalyptic",
                Name = "Post-Apocalyptic",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&romance",
                Name = "Romance",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&sci-fi",
                Name = "Sci-Fi",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&seinen",
                Name = "Seinen",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&shojo",
                Name = "Shojo",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&shonen",
                Name = "Shonen",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&slice_of_life",
                Name = "Slice of life",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;

            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&sport",
                Name = "Sport",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&supernatural",
                Name = "Supernatural",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            channelItemResult.Items.Add(new ChannelItemInfo()
            {
                Id = "category&thriller",
                Name = "Thriller",
                FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                Type = ChannelItemType.Folder,
            });
            channelItemResult.TotalRecordCount++;
            return channelItemResult;
        }

        public async Task<ChannelItemResult> GetCategoryItems(ILogger logger, InternalChannelItemQuery query)
        {
            string source = "<html><body>" + await GetResponse("https://www.crunchyroll.com/videos/anime/genres/ajax_page?pg=0&tagged=" + query.FolderId.Replace("category&", "")) + "</body></html>";
            var config = AngleSharp.Configuration.Default;
            var brow = AngleSharp.BrowsingContext.New(config);
            IDocument document = await brow.OpenAsync(x => x.Content(source));
            ChannelItemResult channelItemResult = new ChannelItemResult();
            foreach (var element in document.GetElementsByTagName("li"))
            {
                string id = element.GetAttribute("id");
                string img = element.GetElementsByTagName("img").First().GetAttribute("src");
                string name = element.GetElementsByTagName("a").First().GetAttribute("title");
                string url = "https://www.crunchyroll.com" + element.GetElementsByTagName("a").First().GetAttribute("href");
                channelItemResult.Items.Add(new ChannelItemInfo()
                {
                    Id = "serie&" + url,
                    ImageUrl = img,
                    Name = name,
                    HomePageUrl = url,
                    Type = ChannelItemType.Folder,
                    FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container
                });
                channelItemResult.TotalRecordCount++;
            }
            return channelItemResult;
        }

        public async Task<ChannelItemResult> GetSerieItems(ILogger logger, InternalChannelItemQuery query)
        {
            string source = await GetResponse(query.FolderId.Replace("serie&", ""));
            var config = AngleSharp.Configuration.Default;
            var brow = AngleSharp.BrowsingContext.New(config);
            IDocument document = await brow.OpenAsync(x => x.Content(source));
            ChannelItemResult channelItemResult = new ChannelItemResult();
            foreach (var element in document.GetElementsByClassName("hover-bubble group-item"))
            {
                string img = element.GetElementsByTagName("img").First().GetAttribute("src");
                string name = element.GetElementsByTagName("a").First().GetAttribute("title");
                string url = "https://www.crunchyroll.com" + element.GetElementsByTagName("a").First().GetAttribute("href");
                channelItemResult.Items.Add(new ChannelItemInfo()
                {
                    Id = url,
                    ImageUrl = img,
                    Name = name,

                    HomePageUrl = url,
                    Type = ChannelItemType.Media,
                    MediaType = MediaBrowser.Model.Channels.ChannelMediaType.Video
                });
                channelItemResult.TotalRecordCount++;
            }
            return channelItemResult;
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