using AngleSharp;
using AngleSharp.Dom;
using MediaBrowser.Controller.Channels;
using n0tFlix.Helpers.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.ComedyCentral
{
    public class Worker
    {
        public Worker()
        {
        }

        public async Task<ChannelItemResult> DownloadShowList()
        {
            string source = await new HttpClient().GetStringAsync("http://www.cc.com/shows");
            var Config = AngleSharp.Configuration.Default;
            var browser = AngleSharp.BrowsingContext.New(Config);
            IDocument document = await browser.OpenAsync(x => x.Content(source));
            var shows = document.GetElementsByClassName("show-item");
            ChannelItemResult channelItemResult = new ChannelItemResult();
            foreach (IElement show in shows)
            {
                string url = show.GetElementsByTagName("a").First().GetAttribute("href");
                string name = show.GetElementsByTagName("a").First().TextContent;
                //              string showSource = await new HttpClient().GetStringAsync(url);
                //                IDocument MainShowPage = await browser.OpenAsync(x => x.Content(showSource));

                //     var images = MainShowPage.GetElementsByTagName("img").First();
                //   string image = images.GetAttribute("src");
                ///string about = MainShowPage.GetElementsByClassName("tier_wrapper").First().TextContent;
                channelItemResult.Items.Add(new ChannelItemInfo()
                {
                    Name = name,
                    Id = name,
                    //    ImageUrl = image,
                    Type = ChannelItemType.Folder,
                    FolderType = MediaBrowser.Model.Channels.ChannelFolderType.Container,
                    HomePageUrl = url,
                    ContentType = MediaBrowser.Model.Channels.ChannelMediaContentType.Clip,
                    //  Overview = about,
                });
                channelItemResult.TotalRecordCount++;
            }
            return channelItemResult;
        }
    }
}