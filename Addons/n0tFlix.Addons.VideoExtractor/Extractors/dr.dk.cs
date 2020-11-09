using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using n0tFlix.Addons.VideoExtractor.Playlists;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class dr_dk : IExtractor
    {
        public string Name => "DrDK";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "dr.dk";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            string source = await client.httpClient.GetStringAsync(url);

            string json = source.GetStringBetween("window.__data = ", "</script>");
            var obje = JObject.Parse(json);
            dynamic page = obje["cache"]["page"].First;
            dynamic entries = page.First["entries"];
            dynamic item = entries.First["item"];
            dynamic offers = item["offers"];
            string resolution = string.Empty;
            string title = page.First["title"];
            string vid = string.Empty;
            string description = item["description"];
            //       string[] kategorier = item["categories"]..ToList();
            long duration = item["duration"];
            //    string[] images = item["images"].ToList();
            foreach (dynamic i in offers)
            {
                if (i["deliveryType"] == "Stream")
                {
                    vid = i["scopes"][0];
                    resolution = i["resolution"];
                }
            }
            string deviceid = Guid.NewGuid().ToString();
            client.httpClient.DefaultRequestHeaders.Add("Referer", url);
            var content = new StringContent($"{{\"deviceId\":\"{deviceid}\",\"scopes\":[\"Catalog\"],\"optout\":true,\"cookieType\":\"Session\"}}", Encoding.UTF8, "application/json");

            var resp = await client.httpClient.PostAsync("https://isl.dr-massive.com/api/authorization/anonymous-sso?device=web_browser&ff=idp%2Cldp&lang=da", content);
            string res = await resp.Content.ReadAsStringAsync();
            string accesstoken = res.GetStringBetween("\"value\":\"", "\",\"type\":\"");
            client.httpClient.DefaultRequestHeaders.Add("authorization", string.Format("Bearer {0}", accesstoken));
            string vide = await client.httpClient.GetStringAsync(string.Format("https://isl.dr-massive.com/api/account/items/{0}/videos?delivery=stream&device=web_browser&ff=idp%2Cldp&lang=da&resolution={1}&sub=Anonymous", vid, resolution));
            vide = vide.Substring(vide.IndexOf("[") + 1);//.Replace("]", "]}");
            vide = "{ \"test\":[" + vide;
            vide = vide.Substring(0, vide.LastIndexOf("]")) + "]}";
            dynamic slutt = JObject.Parse(vide, new JsonLoadSettings() { CommentHandling = CommentHandling.Load, DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace, LineInfoHandling = LineInfoHandling.Load });
            List<DownloadInfo> list = new List<DownloadInfo>();
            foreach (dynamic video in slutt["test"])
            {
                if (video["format"].ToString() == "video/hls")
                {
                    List<SubtitleInfo> subs = new List<SubtitleInfo>();
                    foreach (dynamic sub in video["subtitles"])
                    {
                        subs.Add(new SubtitleInfo()
                        {
                            Language = sub["language"],
                            URL = sub["link"]
                        });
                    }
                    List<VideoInfo> videoInfos = new List<VideoInfo>();
                    videoInfos.Add(new VideoInfo()
                    {
                        id = video["name"],
                        MimeType = EnumHelper.ParseMimeType(video["format"].ToString()),
                        ResolutionType = EnumHelper.ParseResolutionType(video["resolution"].ToString()),
                        url = video["url"]
                    });
                    list.Add(new DownloadInfo()
                    {
                        DownloadId = vid,
                        Subtitles = subs,
                        Videos = videoInfos,
                        Title = title,
                        Duration = duration
                    });
                }
            }
            return list;
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}