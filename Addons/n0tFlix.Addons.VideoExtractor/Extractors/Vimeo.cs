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
    public class Vimeo : IExtractor
    {
        public string Name => "Vimeo";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "vimeo.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            var DownloadId = url.RemoveSubString("https://").RemoveSubString("http://").RemoveSubString("www.")
     .RemoveSubString("vimeo.com/");
            var playerUrl = $"https://player.vimeo.com/video/{DownloadId}";
            var html = await client.httpClient.GetStringAsync(playerUrl);
            var configJson = html.GetStringBetween("var config =", "if (!config.request) {")
                .Trim(" \r\n\t;".ToCharArray());
            var config = JObject.Parse(configJson);

            var video = config.SelectToken("video");
            //var heightX = video.SelectToken("height").ToString().TryToInt();
            //var widthX = video.SelectToken("width").ToString().TryToInt();
            var duration = video.SelectToken("duration").ToString().TryToLong();
            var thumbs = video.SelectToken("thumbs");
            var idX = video.SelectToken("id").ToString();
            var title = video.SelectToken("title").ToString();
            var progressive = config.SelectToken("request.files.progressive").ToList();
            List<DownloadInfo> list = new List<DownloadInfo>();
            List<VideoInfo> videos = new List<VideoInfo>();
            foreach (var p in progressive)
            {
                //var profile = p.SelectToken("profile").ToString();
                var width = p.SelectToken("width").ToString().TryToInt();
                var height = p.SelectToken("height").ToString().TryToInt();
                var mime = p.SelectToken("mime").ToString().ParseMimeType();
                var videoUrl = p.SelectToken("url").ToString();
                var id = p.SelectToken("id").ToString();
                var quality = p.SelectToken("quality").ToString().ParseResolutionType();
                videos.Add(new VideoInfo()
                {
                    id = id,
                    url = videoUrl,
                    ResolutionType = quality,
                    Height = height,
                    Width = width,
                    MimeType = mime,
                });
                var thumb = thumbs.SelectToken(((int)quality).ToString()) + string.Empty;
                var t = p;
            }
            //todo parse the thumbs
            list.Add(new DownloadInfo()
            {
                DownloadId = idX,
                Duration = duration,
                Videos = videos,
                Title = title,
            });
            return list;
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}