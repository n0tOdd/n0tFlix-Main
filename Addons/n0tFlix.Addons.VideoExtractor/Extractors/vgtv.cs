using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using n0tFlix.Addons.VideoExtractor.Extensions;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class vgtv : IExtractor
    {
        public string Name => "vgtv";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "vgtv.no";
        private string ManifestURL = "http://svp.vg.no/svp/api/v1/vgtv/assets/{0}?appName=vgtv-website";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            Regex regex = new Regex("https://www.vgtv.no/video/([0-9]*)/.*", RegexOptions.Compiled);
            var match = regex.Match(url);
            if (match.Success)
            {
                n0tWebClient client = new n0tWebClient();
                string json = await client.httpClient.GetStringAsync(string.Format(ManifestURL, match.Groups[1].Value));
                var stream = JObject.Parse(json);
                List<DownloadInfo> list = new List<DownloadInfo>();
                List<VideoInfo> videoes = new List<VideoInfo>();
                foreach (dynamic vid in stream["streamUrls"])
                {
                    if (vid.Path.Contains("pseudostreaming"))
                        continue;
                    VideoInfo videoInfo = new VideoInfo();
                    videoInfo.id = vid.Name;
                    videoInfo.url = vid.Value;
                    videoInfo.MimeType = videoInfo.id.ParseMimeType();
                    videoes.Add(videoInfo);
                }
                list.Add(new DownloadInfo()
                {
                    DownloadId = stream["id"].ToString(),
                    Description = stream["description"].ToString(),
                    Title = stream["title"].ToString(),
                    Duration = stream["duration"].ToString().TryToLong(),
                    Videos = videoes
                });
                return list;
            }
            else
            {
                return new List<DownloadInfo>();
            }
            // throw new NotImplementedException();
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}