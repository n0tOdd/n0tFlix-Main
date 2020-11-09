using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Utilities;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    //todo add bedre sjekk på url og hent ut litt mer info ang videoene
    public class Viafree : IExtractor
    {
        public string Name => "Viafree";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "viafree";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            await client.OpenDocument(url);
            string source = client.GetSourceString();
            string clearmediaguid = source.GetStringBetween("clear-media-guids\\u002F", "\\u002Fstreams");
            if (string.IsNullOrEmpty(clearmediaguid))
                return null;
            string title = client.Document.Title;
            List<DownloadInfo> list = new List<DownloadInfo>();
            await client.OpenDocument($"https://viafree.mtg-api.com/stream-links/viafree/web/no/clear-media-guids/{clearmediaguid}/streams");
            string json = client.GetSourceString();
            var jo = Newtonsoft.Json.Linq.JObject.Parse(json);
            List<SubtitleInfo> subs = new List<SubtitleInfo>();
            foreach (var aa in jo["embedded"]["subtitles"])
            {
                var data = aa["data"];
                var link = aa["link"];
                subs.Add(new SubtitleInfo()
                {
                    Language = data["language"].ToString(),
                    URL = link["href"].ToString(),
                });
            }
            List<VideoInfo> videos = new List<VideoInfo>();
            foreach (var aa in jo["embedded"]["prioritizedStreams"])
            {
                var data = aa["links"]["stream"]["href"].ToString();
                videos.Add(new VideoInfo()
                {
                    id = "id",
                    url = data,
                });
            }
            list.Add(new DownloadInfo() { DownloadId = clearmediaguid, Subtitles = subs, Videos = videos });
            return list;
        }

        public async Task<bool> Login(string id, string pw)
          => true;
    }
}