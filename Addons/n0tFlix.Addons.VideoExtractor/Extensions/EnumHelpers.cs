using n0tFlix.Addons.VideoExtractor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Extensions
{
    public static class EnumHelper
    {
        public static VideoMimeTypes ParseMimeType(this string type)
        {
            if (string.IsNullOrEmpty(type)) return VideoMimeTypes.None;
            if (type == "application/x-mpegURL") return VideoMimeTypes.MPEG;

            if (type.Contains("video/mp4")) return VideoMimeTypes.Mp4;
            if (type.Contains("video/webm")) return VideoMimeTypes.WebMVideo;
            if (type.Contains("video/3gpp")) return VideoMimeTypes._3GPP;
            if (type.Contains("hds")) return VideoMimeTypes.HDS;
            if (type.Contains("hls")) return VideoMimeTypes.HLS;
            if (type.Contains("mp4")) return VideoMimeTypes.Mp4;
            if (type.Contains("video/hls")) return VideoMimeTypes.HLS;
            if (type.Contains("audio/webm")) return VideoMimeTypes.WebMAudio;
            if (type.Contains("mp3")) return VideoMimeTypes.Mp3;

            throw new Exception("not implemented yet " + type);
        }

        public static VideoResolutionTypes ParseResolutionType(this string quality)
        {
            if (string.IsNullOrEmpty(quality)) return VideoResolutionTypes.P0;

            switch (quality)
            {
                case "1440p": return VideoResolutionTypes.P1440;
                case "2160p": return VideoResolutionTypes.P2160;
                case "1080p": return VideoResolutionTypes.P1080;
                case "720p": return VideoResolutionTypes.P720;
                case "540p": return VideoResolutionTypes.P540;
                case "480p": return VideoResolutionTypes.P480;
                case "360p": return VideoResolutionTypes.P360;
                case "240p": return VideoResolutionTypes.P240;
                case "144p": return VideoResolutionTypes.P144;
                case "HD-1080": return VideoResolutionTypes.P1080;
                //quality
                case "hd1080": return VideoResolutionTypes.P1080;

                case "hd720": return VideoResolutionTypes.P720;
                case "large": return VideoResolutionTypes.P1080;
                case "medium": return VideoResolutionTypes.P480;
                case "small": return VideoResolutionTypes.P240;
                case "tiny": return VideoResolutionTypes.P144;

                case "sd": return VideoResolutionTypes.SD;
                case "hd": return VideoResolutionTypes.HD;
            }

            throw new Exception("not implemented yet " + quality);
        }
    }
}