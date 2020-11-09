using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class Youtube : IExtractor
    {
        public string Name => "Youtube";
        public string Description => "A youtube extractor for use on youtube.com";

        public async Task<bool> Login(string id, string pw)
         => true;

        public string BaseURL = "youtube.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        private bool TryNormalizeYoutubeUrl(string url, out string normalizedUrl, out string DownloadId)
        {
            url = url.Trim();

            url = url.Replace("youtu.be/", "youtube.com/watch?v=");
            url = url.Replace("www.youtube", "youtube");
            url = url.Replace("youtube.com/embed/", "youtube.com/watch?v=");

            if (url.Contains("/v/"))
            {
                url = "http://youtube.com" + new Uri(url).AbsolutePath.Replace("/v/", "/watch?v=");
            }

            url = url.Replace("/watch#", "/watch?");
            var query = Httphelpers.ParseQueryString(url);
            if (!query.TryGetValue("v", out DownloadId))
            {
                normalizedUrl = null;
                return false;
            }
            normalizedUrl = "http://youtube.com/watch?v=" + DownloadId;

            return true;
        }

        private JObject LoadJson(string url)
        {
            var pageSource = new WebClient().DownloadString(url);
            const string unavailableContainer = "<div id=\"watch-player-unavailable\">";

            var isUnavailable = pageSource.Contains(unavailableContainer);
            if (isUnavailable)
            {
                throw new Exception("VideoNotAvailable");
            }

            var dataRegex = new Regex(@"ytplayer\.config\s*=\s*(\{.+?\});", RegexOptions.Multiline);

            var m = dataRegex.Match(pageSource);
            if (m.Success)
            {
                var extractedJson = m.Result("$1");
                return JObject.Parse(extractedJson);
            }

            return null;
        }

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            var isYoutubeUrl = TryNormalizeYoutubeUrl(url, out url, out var DownloadId);
            if (!isYoutubeUrl)
            {
                return null;
            }
            var json = LoadJson(url);
            if (json == null)
            {
                return null;
            }

            //get title
            var js = JObject.Parse(json["args"]["player_response"].ToString());
            var Details = JObject.Parse(js["videoDetails"].ToString());
            string author = Details["author"].ToString();
            string videoId = Details["videoId"].ToString();
            string ChannelID = Details["channelId"].ToString();
            string Description = Details["shortDescription"].ToString();
            string views = Details["viewCount"].ToString();
            string[] Keywords = Details["keywords"].ToString().Replace("{[", "").Replace("[", "").Replace("]}", "").Replace("\"", "").Replace("\r\n", "").Split(",");
            List<ImageInfo> Images = new List<ImageInfo>();
            foreach (dynamic image in Details["thumbnail"]["thumbnails"])
            {
                Images.Add(new ImageInfo() { url = image["url"].ToString(), id = Utils.GenerateNewGuidString() });
            }
            var videoTitle = Details["title"].ToString() ?? js?.ToString() ?? string.Empty;
            var downloadUrls = JObject.Parse(js["streamingData"].ToString());
            var adaptiveFormats = downloadUrls["adaptiveFormats"];
            List<DownloadInfo> list = new List<DownloadInfo>();

            List<VideoInfo> info = new List<VideoInfo>();
            foreach (var see in adaptiveFormats)
            {
                const string signatureQuery = "signature";
                const string rateBypassFlag = "ratebypass";
                IDictionary<string, string> queries = null;
                if (see["signatureCipher"] != null)
                {
                    var uu = see["signatureCipher"].ToString();

                    queries = Httphelpers.ParseQueryString(uu);
                    if (queries.ContainsKey("s") || queries.ContainsKey("sig"))
                    {
                        var signature = queries.ContainsKey("s") ? queries["s"] : queries["sp"];
                        url = $"{queries["url"]}&{signatureQuery}={signature}";
                        var fallbackHost = queries.ContainsKey("fallback_host") ? "&fallback_host=" + queries["fallback_host"] : string.Empty;
                        url += fallbackHost;
                    }
                    else
                    {
                        url = queries["url"];
                    }
                }
                else
                {
                    url = see["url"].ToString();
                    queries = Httphelpers.ParseQueryString(url);
                }
                url = Httphelpers.UrlDecode(url);
                var parameters = Httphelpers.ParseQueryString(url);

                if (!parameters.ContainsKey(rateBypassFlag))
                    url += $"&{rateBypassFlag}=yes";

                var quality = see["quality"] ?? see["quality_label"];

                queries.TryGetValue("type", out var type);
                if (string.IsNullOrEmpty(type))
                    queries.TryGetValue("mime", out type);
                //     if (!string.IsNullOrEmpty(type) && type.Contains("audio/")) continue;

                queries.TryGetValue("size", out var size);
                int w = 0, h = 0;
                if (size != null)
                {
                    var wh = size.Split('x').Select(int.Parse).ToArray();
                    w = wh[0];
                    h = wh[1];
                }
                info.Add(new VideoInfo()
                {
                    id = DownloadId,
                    ResolutionType = EnumHelper.ParseResolutionType(quality.ToString()),
                    MimeType = EnumHelper.ParseMimeType(type),
                    url = url
                });
            }

            var formats = downloadUrls["formats"];
            foreach (var see in formats)
            {
                const string signatureQuery = "signature";
                const string rateBypassFlag = "ratebypass";
                IDictionary<string, string> queries = null;
                if (see["signatureCipher"] != null)
                {
                    var uu = see["signatureCipher"].ToString();

                    queries = Httphelpers.ParseQueryString(uu);
                    if (queries.ContainsKey("s") || queries.ContainsKey("sig"))
                    {
                        var signature = queries.ContainsKey("s") ? queries["s"] : queries["sp"];
                        url = $"{queries["url"]}&{signatureQuery}={signature}";
                        var fallbackHost = queries.ContainsKey("fallback_host") ? "&fallback_host=" + queries["fallback_host"] : string.Empty;
                        url += fallbackHost;
                    }
                    else
                    {
                        url = queries["url"];
                    }
                }
                else
                {
                    url = see["url"].ToString();
                    queries = Httphelpers.ParseQueryString(url);
                }
                url = Httphelpers.UrlDecode(url);
                var parameters = Httphelpers.ParseQueryString(url);

                if (!parameters.ContainsKey(rateBypassFlag))
                    url += $"&{rateBypassFlag}=yes";

                var quality = see["quality"] ?? see["quality_label"];

                queries.TryGetValue("type", out var type);
                if (string.IsNullOrEmpty(type))
                    queries.TryGetValue("mime", out type);
                //       if (!string.IsNullOrEmpty(type) && type.Contains("audio/")) continue;

                queries.TryGetValue("size", out var size);
                int w = 0, h = 0;
                if (size != null)
                {
                    var wh = size.Split('x').Select(int.Parse).ToArray();
                    w = wh[0];
                    h = wh[1];
                }
                info.Add(new VideoInfo()
                {
                    id = DownloadId,
                    ResolutionType = EnumHelper.ParseResolutionType(quality.ToString()),
                    MimeType = EnumHelper.ParseMimeType(type),
                    url = url
                });
            }
            list.Add(new DownloadInfo() { DownloadId = DownloadId, Images = Images, Description = Description, Title = videoTitle, Tags = Keywords.ToList(), Videos = info });
            return list;
        }
    }
}