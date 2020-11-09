using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using n0tFlix.Addons.VideoExtractor.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class DailyMotion : IExtractor
    {
        public string Name => "Dailymotion";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "dailymotion.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            var html = await client.httpClient.GetStringAsync(url);
            var jsonMetaData = html.GetStringBetween("__PLAYER_CONFIG__ = ", ";</script>");
            var jObj = JObject.Parse(jsonMetaData);
            var metadataTemplateUrl = jObj.SelectToken("context").SelectToken("metadata_template_url").ToString();
            var embedder = jObj.SelectToken("context").SelectToken("embedder").ToString();
            var DownloadId = embedder.RemoveSubString("https://").RemoveSubString("http://").RemoveSubString("www.")
                .RemoveSubString("dailymotion.com/video/");
            metadataTemplateUrl = metadataTemplateUrl.Replace(":DownloadId", DownloadId);
            var jsonMetaData2 = await client.httpClient.GetStringAsync(metadataTemplateUrl);
            jObj = JObject.Parse(jsonMetaData2);
            var filmstripUrl = jObj.SelectToken("filmstrip_url").ToString();
            //no need to this
            // ReSharper disable once UnusedVariable
            //    var posterUrl = jObj.SelectToken("poster_url").ToString();//small thumbnail

            //in seconds
            var duration = jObj.SelectToken("duration").ToString().TryToInt();
            var id = jObj.SelectToken("id").ToString(); //check if the same
            var title = jObj.SelectToken("title").ToString();

            var posters = jObj.SelectToken("posters");
            var postersDictionary = posters.Cast<JProperty>()
                .ToDictionary(poster => poster.Name.TryToInt(), poster => poster.Value.ToString());

            var qualities = jObj.SelectToken("qualities");
            List<DownloadInfo> list = new List<DownloadInfo>();
            List<VideoInfo> videoInfos = new List<VideoInfo>();
            List<ImageInfo> images = new List<ImageInfo>();
            foreach (var jToken in qualities)
            {
                var poster = (JProperty)jToken;
                var resolution = poster.Name.TryToInt();
                var type = poster.Values().First().First().Values().First().ToString();
                var urlx = poster.Values().First().Skip(1).First().Values().First().ToString();

                postersDictionary.TryGetValue(resolution, out var thumbnailUrl);
                images.Add(new ImageInfo()
                {
                    url = thumbnailUrl,
                    id = Utils.GenerateNewGuidString(),
                    Height = "Unknown",
                    Width = "Unknown",
                });
                images.Add(new ImageInfo()
                {
                    url = filmstripUrl,
                    id = Utils.GenerateNewGuidString(),
                    Height = "Unknown",
                    Width = "Unknown",
                });

                videoInfos.Add(new VideoInfo()
                {
                    id = id,
                    MimeType = type.ParseMimeType(),
                    url = urlx,
                    ResolutionType = (VideoResolutionTypes)resolution,
                });
            }
            list.Add(new DownloadInfo
            {
                DownloadId = id,
                Title = title,
                Duration = duration,
                Videos = videoInfos,
                Description = "Unknown",
                Images = images
            });
            return list;
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}