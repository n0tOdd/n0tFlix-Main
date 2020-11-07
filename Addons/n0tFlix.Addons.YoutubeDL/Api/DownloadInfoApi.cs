using Jellyfin.Data.Entities;
using MediaBrowser.Model.Services;
using Microsoft.Extensions.Logging;
using n0tFlix.Addons.YoutubeDL.Helpers;
using n0tFlix.Addons.YoutubeDL.Models;
using n0tFlix.Addons.YoutubeDL.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace n0tFlix.Addons.YoutubeDL
{
    [Route("/YoutubeDL/DownloadInfo", "GET", Description = "Gets Streamlink / download link from a webpage")]
    public class GetJson : IReturn<string>
    {
        [ApiMember(Name = "Url", Description = "Url", IsRequired = true, DataType = "string",
          ParameterType = "query", Verb = "GET")]
        public string Url { get; set; }
    }

    public class DownloadInfoApi : IService
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
        public string Get(GetJson getURL)
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