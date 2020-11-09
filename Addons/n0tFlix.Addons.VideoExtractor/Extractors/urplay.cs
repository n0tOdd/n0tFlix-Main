using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class urplay : IExtractor
    {
        public string Name => "Urplay";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "urplay.se";

        public bool CheckURL(string url)
            => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            string source = await client.httpClient.GetStringAsync(url);
            Regex regex = new Regex("/Player/Player\" data-react-props=\"([^\"]+)\\\"", RegexOptions.Compiled);
            Match match = regex.Match(source);
            if (match.Success)
            {
                string json = match.Groups[1].Value.Replace("&quot;", "\"");
                dynamic Js = Newtonsoft.Json.Linq.JObject.Parse(json);
                string loader = await client.httpClient.GetStringAsync("https://streaming-loadbalancer.ur.se/loadbalancer.json");
                dynamic loadbalance = JObject.Parse(loader);
                string urlbase = loadbalance["redirect"];
                List<VideoInfo> videos = new List<VideoInfo>();
                List<DownloadInfo> DownloadInfos = new List<DownloadInfo>();
                foreach (dynamic stream in Js["currentProduct"]["streamingInfo"])
                {
                    if (stream.First["default"].ToString() == "True")
                    {
                        string descript = Js["currentProduct"]["description"].ToString();
                        string mainTitle = Js["currentProduct"]["mainTitle"].ToString();
                        string title = Js["currentProduct"]["title"].ToString();

                        string sd = string.Format("https://{0}/{1}playlist.m3u8", urlbase, stream.First["sd"]["location"].ToString());
                        string hd = string.Format("https://{0}/{1}playlist.m3u8", urlbase, stream.First["hd"]["location"].ToString());
                        videos.Add(new VideoInfo() { id = "sd", url = sd, ResolutionType = VideoResolutionTypes.SD });
                        videos.Add(new VideoInfo() { id = "hd", url = hd, ResolutionType = VideoResolutionTypes.HD });
                        DownloadInfos.Add(new DownloadInfo()
                        {
                            Description = descript,
                            Videos = videos,
                            Title = mainTitle + " " + title,
                            DownloadId = title,
                        });
                    }
                }
                return DownloadInfos;
                System.Threading.Thread.Sleep(10);
            }
            return null;
        }

        public async Task<bool> Login(string id, string pw)
            => true;
    }
}