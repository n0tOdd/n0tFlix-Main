using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
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
    public class Pornhub : IExtractor
    {
        public string Name => "Pornhub";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "pornhub.com";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        private IEnumerable<string> DecodeURL(string source)
        {
            source = source.Substring(source.IndexOf("document.referrer.split('/')[2]") + "document.referrer.split('/')[2]".Length);
            source = source.Substring(0, source.IndexOf("if (utmSource == ''"));

            string[] variables = source.Split(new[] { "var" }, StringSplitOptions.None);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string ferdig = string.Empty;
            List<string> StreamURL = new List<string>();
            foreach (string variable in variables)
            {
                if (variable.Contains("mediaDefinitions"))
                {
                    if (string.IsNullOrEmpty(ferdig))
                        continue;
                    if (ferdig.Contains(".mp4?validfrom=1603404"))
                        continue;
                    StreamURL.Add(ferdig);
                    ferdig = string.Empty;
                    continue;
                }
                if (variable.StartsWith("mp4") || variable.StartsWith("hls") || variable.StartsWith(" mp4") || variable.StartsWith(" hls"))
                {
                    string cleanup = variable;
                    MatchCollection matches = Regex.Matches(cleanup, @"(?<name>\/\* \+ .*? \+ \*\/)");
                    foreach (Match match in matches)
                    {
                        cleanup = cleanup.Replace(match.Value, "");
                    }
                    cleanup = cleanup.Replace("mp4480p=", "");
                    cleanup = cleanup.Replace("hls480p=", "");
                    cleanup = cleanup.Replace("hls480=", "");
                    cleanup = cleanup.Replace("mp4480p", "");
                    cleanup = cleanup.Replace("hls480p", "");
                    cleanup = cleanup.Replace("hls480", "");
                    cleanup = cleanup.Replace(";flash", "");

                    foreach (string t in cleanup.Split("+"))
                    {
                        if (t.Contains("mediaDefinitions"))
                        {
                            StreamURL.Add(ferdig);
                            ferdig = string.Empty;
                            continue;
                        }
                        string clean = t;
                        while (clean.Contains(" "))
                            clean = clean.Replace(" ", "");
                        while (clean.Contains(";"))
                            clean = clean.Replace(";", "");
                        if (keyValuePairs.ContainsKey(clean))
                        {
                            ferdig = ferdig + keyValuePairs[clean];
                        }
                    }
                }
                if (!variable.Contains("="))
                    continue;
                string first = variable.Substring(0, variable.IndexOf("="));
                string sec = variable.Substring((variable.IndexOf("=") + 1), variable.Length - (variable.IndexOf("=") + 1));
                keyValuePairs.Add(first.Replace(" ", ""), sec.Replace(" ", "").Replace("\" + \"", "").Replace("\"", "").Replace(";", "").Replace("+", ""));
            }

            foreach (string uri in StreamURL)
            {
                yield return uri;
            }
        }

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();

            var query = Httphelpers.ParseQueryString(url);
            var DownloadId = query["viewkey"];
            var rawUrl = $"https://www.pornhub.com/view_video.php?viewkey={DownloadId}";
            var html = await client.httpClient.GetStringAsync(rawUrl);
            var duration = html.GetStringBetween("<meta property=\"video:duration\" content=\"", "\"").TryToInt();
            var width = html.GetStringBetween("<meta name=\"twitter:player:width\" content=\"", "\"").TryToInt();
            var height = html.GetStringBetween("<meta name=\"twitter:player:height\" content=\"", "\"").TryToInt();

            var embedUrl = $"https://www.pornhub.com/embed/{DownloadId}";
            html = await client.httpClient.GetStringAsync(embedUrl);
            var title = html.GetStringBetween("<title>", "</title>");
            var flashVarsJson = html.GetStringBetween("var flashvars =", "utmSource");
            flashVarsJson = flashVarsJson.Trim(" \t\r\n,".ToCharArray());
            var jObj = JObject.Parse(flashVarsJson);
            var image_url = jObj.SelectToken("image_url").ToString();
            var video_title = jObj.SelectToken("video_title").ToString();//check if the same
            var defaultQuality = jObj.SelectToken("defaultQuality").Values<int>().ToArray();//check if the same
            var mediaDefinitions = jObj.SelectToken("mediaDefinitions");//check if the same
            List<DownloadInfo> list = new List<DownloadInfo>();
            List<ImageInfo> images = new List<ImageInfo>();
            images.Add(new ImageInfo() { Height = "Unknow", Width = "Unknown", id = Utils.GenerateNewGuidString(), url = image_url });
            List<VideoInfo> videos = new List<VideoInfo>();

            var uri = DecodeURL(html);
            foreach (var mediaDefinition in uri)
            {
                try
                {
                    videos.Add(new VideoInfo()
                    {
                        Width = width,
                        Height = height,
                        // ResolutionType = (VideoResolutionTypes)quality,
                        url = mediaDefinition,
                        id = DownloadId,
                    });
                }
                catch (Exception ex)
                {
                }
            }
            list.Add(new DownloadInfo() { DownloadId = DownloadId, Title = title, Images = images, Videos = videos, Duration = duration });
            return list;
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}