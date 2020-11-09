using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class anyanime : IExtractor
    {
        public string Name => "AnyAnime";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "anyanime.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        //todo add more complete information extraction, get somebody knowing arabic to help me
        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            await client.OpenDocument(url);
            var iframes = client.Document.GetElementsByTagName("iframe");
            string title = client.Document.Title;
            string sou = client.GetSourceString();
            string src = string.Empty;
            foreach (var iframe in iframes)
            {
                if (iframe.HasAttribute("src"))
                {
                    string tmp = iframe.GetAttribute("src");
                    if (tmp.StartsWith("//"))
                    {
                        src = tmp.Replace("//", "");
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(src))
                return new List<DownloadInfo>();

            await client.OpenDocument("https://" + src);
            sou = client.GetSourceString();
            string source = await client.httpClient.GetStringAsync("https://" + src);
            string json = sou.GetStringBetween("data-options=\"", "\" data-player-container").Replace("&quot", "\"");
            string hd = json.GetStringBetween("hd", "seekSchema");
            hd = hd.GetStringBetween("\\\";,\\\";url\\\";:\\\";", "\\\";,\\\";");
            hd = hd.Replace("\\\\u0026", "&");
            return new List<DownloadInfo>() { new DownloadInfo() { DownloadId = title, Title = title, Videos = new List<VideoInfo>() { new VideoInfo() { id = title, url = hd } } } };
        }

        public async Task<bool> Login(string id, string pw)
            => true;
    }
}