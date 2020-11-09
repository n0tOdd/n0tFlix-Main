using n0tFlix.Addons.VideoExtractor.Extensions;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using n0tFlix.Addons.VideoExtractor.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class Xnxx : IExtractor
    {
        public string Name => "Xnxx";
        public string Description => "A Xnxx extractor for use on xnxx.com";

        private string URLmatch = "xnxx.com";

        public bool CheckURL(string url)
             => url.Contains(URLmatch, StringComparison.OrdinalIgnoreCase);

        public async Task<List<DownloadInfo>> Extract(string url)
        {
            n0tWebClient client = new n0tWebClient();
            client.OpenDocument(url).Wait();
            var html = client.GetSourceString();

            var urlHLSVideo = html.GetStringBetween("html5player.setVideoHLS(\'", "');");
            var thumbSlideMinute = html.GetStringBetween("html5player.setThumbSlideMinute(\'", "\');");
            var thumbImage = html.GetStringBetween("<meta property=\"og:image\" content=\"", "\" />");
            // ReSharper restore UnusedVariable

            var urlLowVideo = html.GetStringBetween("html5player.setVideoUrlLow(\'", "');");
            var urlHighVideo = html.GetStringBetween("html5player.setVideoUrlHigh(\'", "\');");

            var thumbUrl = html.GetStringBetween("html5player.setThumbUrl(\'", "\');");
            var thumbUrl169 = html.GetStringBetween("html5player.setThumbUrl169('", "\');");
            var thumbSlide = html.GetStringBetween("html5player.setThumbSlide('", "\');");
            var thumbSlideBig = html.GetStringBetween("html5player.setThumbSlideBig(\'", "\');");

            var DownloadId = html.GetStringBetween("<meta property=\"og:video\" content=\"", "\" />").FromThisToEnd("id_video=");
            var videoTitle = html.GetStringBetween("html5player.setVideoTitle(\'", "\');");

            var duration = html.GetStringBetween("<meta property=\"og:duration\" content=\"", "\" />").TryToLong();
            var width = html.GetStringBetween("<meta property=\"og:video:width\" content=\"", "\" />").TryToInt();
            var height = html.GetStringBetween("<meta property=\"og:video:height\" content=\"", "\" />").TryToInt();
            List<DownloadInfo> videos = new List<DownloadInfo>();
            List<VideoInfo> videoInfos = new List<VideoInfo>() { { new VideoInfo() { id = Utils.GenerateNewGuidString(), Height = height, Width = width, url = urlHighVideo } } };
            List<ImageInfo> imageInfo = new List<ImageInfo>() { { new ImageInfo() { id = Utils.GenerateNewGuidString(), url =  thumbSlideBig } },
            { new ImageInfo() { id = Utils.GenerateNewGuidString(), url =  thumbSlide } }};

            DownloadInfo DownloadInfo = new DownloadInfo() { Duration = duration, Title = videoTitle, DownloadId = Utils.GenerateNewGuidString(), OriginalURL = url, Images = imageInfo, Videos = videoInfos, };
            videos.Add(DownloadInfo);
            return videos;
        }

        public async Task<bool> Login(string id, string pw)
            => true;
    }
}