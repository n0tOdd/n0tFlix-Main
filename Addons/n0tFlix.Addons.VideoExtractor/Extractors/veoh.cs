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
    public class veoh : IExtractor
    {
        public string Name => "Veoh";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "veoh.com";
        private n0tWebClient client = new n0tWebClient();

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            //     url = url.Replace("/watch/", "/watch/getVideo/");
            var s = await client.httpClient.GetAsync(url);
            string source = await s.Content.ReadAsStringAsync();
            //      string htmltext = client.GetSourceString();
            JObject VLinks = JObject.Parse(source);
            string title = VLinks["video"]["title"].ToString();
            string description = VLinks["video"]["description"].ToString();
            List<string> Keyword = VLinks["video"]["tags"].ToString().Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            JObject src = (JObject)VLinks["video"]["src"];
            List<ImageInfo> images = new List<ImageInfo>();
            images.Add(new ImageInfo()
            {
                id = VLinks["video"]["permalinkId"].ToString(),
                url = VLinks["video"]["src"]["poster"].ToString(),
            });
            List<VideoInfo> videoInfos = new List<VideoInfo>();
            videoInfos.Add(new VideoInfo()
            {
                id = VLinks["video"]["permalinkId"].ToString(),
                url = VLinks["video"]["src"]["HQ"].ToString(),
                ResolutionType = EnumHelper.ParseResolutionType("HQ")
            });
            videoInfos.Add(new VideoInfo()
            {
                id = VLinks["video"]["permalinkId"].ToString(),
                url = VLinks["video"]["src"]["Regular"].ToString(),
                ResolutionType = EnumHelper.ParseResolutionType("sd")
            });

            return new List<DownloadInfo>()
            {
                new DownloadInfo()
                {
                      DownloadId =   VLinks["video"]["permalinkId"].ToString(),
                      Images = images,
                      Videos = videoInfos,
                       Title = title,
                     Description = description,
                      Tags = Keyword,
                     OriginalURL = url,
                }
            };
        }

        public async Task<bool> Login(string id, string pw)
            => true;
    }
}