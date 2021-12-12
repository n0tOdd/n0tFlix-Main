using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using n0tFlix.Addons.YoutubeDL.Helpers;
using System.IO;
using System.Runtime.InteropServices;

namespace n0tFlix.Addons.YoutubeDL
{
    public class YoutubeDLService
    {

    }
    public class GetJson
    {
        public string Url { get; set; }
    }

    [ApiController]
    public class DownloadInfoApi : ControllerBase
    {
        private readonly ILogger<DownloadInfoApi> logger;

        public DownloadInfoApi(ILogger<DownloadInfoApi> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Checks for the download info with youtube-dl but only prints the json back to you, never saves anything
        /// </summary>
        /// <param name="getURL"></param>
        /// <returns></returns>
        [Route("DownloadInfoApi/{GetJson}")]
        public ActionResult<string> Get(GetJson getURL)
        {
            string path = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Plugin.Instance.DataFolderPath, "youtube-dl.exe");
            else
                path = Path.Combine(Plugin.Instance.DataFolderPath, "youtube-dl");

            string retur = string.Empty;
            YoutubeDL youtubeDL = new YoutubeDL(path);
            youtubeDL.Options.VerbositySimulationOptions.Verbose = true;
            youtubeDL.Options.VerbositySimulationOptions.GetUrl = true;
            youtubeDL.Options.VerbositySimulationOptions.SkipDownload = true;
            youtubeDL.Options.VerbositySimulationOptions.Simulate = true;
            youtubeDL.Options.GeoRestrictionOptions.GeoBypass = true;
            youtubeDL.Options.GeneralOptions.AbortOnError = false;
            youtubeDL.Options.GeneralOptions.IgnoreErrors = true;
            youtubeDL.Options.DownloadOptions.Retries = 10;
            youtubeDL.Options.VideoFormatOptions.Format = Enums.VideoFormat.best;
            youtubeDL.VideoUrl = getURL.Url;
            youtubeDL.StandardOutputEvent += (object sender, string e) => retur += e;

            youtubeDL.Download(getURL.Url);
            return retur; ;
        }

        /// <summary>
        /// Downloads the video and saves it at the path selected, if path does not exist it wil cancel the download
        /// </summary>
        /// <param name="getURL"></param>
        /// <returns></returns>
    }
}