using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Models
{
    public class DownloadInfo
    {
        /// <summary>
        /// Id of the current Download class (Just use a the Utils.GenerateNewGuidString() or if the page have a custom id give it that
        /// </summary>
        public string DownloadId { get; set; }

        /// <summary>
        /// Title of the song, video , Image or wtf we are extracting
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description found about our download
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The playtime of our video/audio files
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// The url we extracted all the data from
        /// </summary>
        public string OriginalURL { get; set; }

        /// <summary>
        /// Genres matching the audio/video / image or other data found
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// Tags/Keywords or some other shit about the downloads
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Collections of the videos/subtitles /images/audio collections we are downloadin
        /// </summary>
        public List<VideoInfo> Videos { get; set; }

        public List<SubtitleInfo> Subtitles { get; set; }
        public List<ImageInfo> Images { get; set; }
    }
}