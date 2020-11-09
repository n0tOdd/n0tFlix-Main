using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class TubiTv : IExtractor
    {
        public string Name => "TubiTv";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "tubitv.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();

            await client.OpenDocument(url);
            System.Threading.Thread.Sleep(2000);
            string json = client.GetSourceString().GetStringBetween("window.__data=", ";</script>");
            var check = JObject.Parse(json);
            List<SubtitleInfo> subs = new List<SubtitleInfo>();
            List<DownloadInfo> list = new List<DownloadInfo>();
            List<VideoInfo> videos = new List<VideoInfo>();
            List<ImageInfo> images = new List<ImageInfo>();
            foreach (var vid in check["video"]["byId"])
            {
                var vi = vid.First;
                if (url.Contains(vi["id"].ToString()))
                {
                    foreach (var sub in vi["subtitles"])
                    {
                        string lang = sub["lang"].ToString();
                        string surl = sub["url"].ToString();
                        subs.Add(new SubtitleInfo()
                        {
                            Language = lang,
                            URL = surl
                        });
                    }
                    string ur = vi["url"].ToString();
                    videos.Add(new VideoInfo()
                    {
                        id = vi["id"].ToString(),
                        url = ur,
                    });
                    images.Add(new ImageInfo()
                    {
                        id = vi["id"].ToString(),
                        url = vi["thumbnails"].First.ToString().Replace("//", "")
                    });
                }
            }
            //todo add description og runtim
            list.Add(new DownloadInfo() { DownloadId = Utilities.Utils.GenerateNewGuidString(), Images = images, Subtitles = subs, Videos = videos, });
            return list;
        }

        public async Task<bool> Login(string id, string pw)
        => true;
    }
}