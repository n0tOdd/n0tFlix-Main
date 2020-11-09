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
    public class NRK : IExtractor
    {
        public string Name => "NRK";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "tv.nrk.no";
        private string MetaDataBase = "https://psapi.nrk.no/playback/manifest/program/{0}";

        public bool CheckURL(string url)
            => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        //todo fix bedre så den velger riktig episode/program
        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            string source = await client.httpClient.GetStringAsync(url);
            string json = source.GetStringBetween("window.__NRK_TV_SERIES_INITIAL_DATA_V2__ =", "};") + "}";
            dynamic ob = JObject.Parse(json, new JsonLoadSettings() { CommentHandling = CommentHandling.Ignore, DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Ignore, LineInfoHandling = LineInfoHandling.Ignore });
            dynamic state = ob.initialState;
            dynamic serie = state.series;
            dynamic season = serie.seasons.First;
            dynamic ep = season.episodes.First;
            string id = ep.prfId;
            string ManifestJson = await client.httpClient.GetStringAsync(string.Format(MetaDataBase, id));
            dynamic manifest = JObject.Parse(ManifestJson);
            List<SubtitleInfo> subs = new List<SubtitleInfo>();
            foreach (dynamic sub in manifest.playable.subtitles)
            {
                subs.Add(new SubtitleInfo()
                {
                    Language = sub.language,
                    URL = sub.webVtt,
                });
            }
            List<VideoInfo> list = new List<VideoInfo>();
            foreach (dynamic vid in manifest.playable.assets)
            {
                list.Add(new VideoInfo()
                {
                    id = manifest.id,
                    MimeType = vid.format,
                    url = vid.url,
                });
            }

            return new List<DownloadInfo>() { new DownloadInfo() {
             DownloadId = manifest.id,
              Videos = list,
               Subtitles = subs,
               Title = ep.titles.title
            } };
        }

        public async Task<bool> Login(string id, string pw)
            => true;
    }
}