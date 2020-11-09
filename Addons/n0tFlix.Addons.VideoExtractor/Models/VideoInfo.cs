using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Models
{
    public class VideoInfo
    {
        public string id { get; set; }
        public string url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public VideoResolutionTypes ResolutionType { get; set; }
        public VideoMimeTypes MimeType { get; set; }
    }
}