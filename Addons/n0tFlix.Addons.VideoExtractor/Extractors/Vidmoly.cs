using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    /// <summary>
    /// Todo add login
    /// </summary>
    public class Vidmoly : IExtractor
    {
        public string Name => "Videmoly";
        public string Description => "A " + Name + " extractor for use on " + BaseURL;

        private string BaseURL = "vidmoly.me";

        public bool CheckURL(string url)
         => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            var html = await client.httpClient.GetStringAsync(url);
            //embed video url in iframeSrc
            var iframeSrc = html.GetStringBetween("<iframe src=\"", "\"");
            if (iframeSrc.StartsWith("//")) iframeSrc = "http:" + iframeSrc;
            //get video id
            var DownloadId = iframeSrc.RemoveSubString("https://vidmoly.me/")
                .RemoveSubString("https://")
                .RemoveSubString("http://")
                .RemoveSubString("vidmoly.me/embed-")
                .RemoveSubString(".html");

            html = await client.httpClient.GetStringAsync(iframeSrc);
            var label = html.GetStringBetween("|label|", "|");

            //TODO get Hash & get direct download url instead
            //var hash = "";
            //var newUrl =$"https://vidmoly.me/dl?op=download_orig&id={DownloadId}&mode=&hash={hash}";
            //html = client.DownloadStringFromUrl(newUrl);
            var videoUrl = $"//144.mokalix.tk/{label}/v.mp4";

            return new List<DownloadInfo>()
            {
                new DownloadInfo()
                {
                    DownloadId = DownloadId,
                    Videos = new List<VideoInfo>(){  new VideoInfo(){ url = videoUrl, id = DownloadId } },
                }
            };
        }

        public async Task<bool> Login(string id, string pw)
         => true;
    }
}