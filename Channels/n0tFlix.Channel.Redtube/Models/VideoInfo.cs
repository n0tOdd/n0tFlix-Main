using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Redtube.Models
{
    public class VideoInfo
    {
        public class Thumb
        {
            [JsonProperty("size")]
            public string Size { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }

            [JsonProperty("height")]
            public int Height { get; set; }

            [JsonProperty("src")]
            public string Src { get; set; }
        }

        public class Tag
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }
        }

        public class Video2
        {
            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("views")]
            public int Views { get; set; }

            [JsonProperty("video_id")]
            public string VideoId { get; set; }

            [JsonProperty("rating")]
            public object Rating { get; set; }

            [JsonProperty("ratings")]
            public int Ratings { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("embed_url")]
            public string EmbedUrl { get; set; }

            [JsonProperty("default_thumb")]
            public string DefaultThumb { get; set; }

            [JsonProperty("thumb")]
            public string Thumb { get; set; }

            [JsonProperty("publish_date")]
            public string PublishDate { get; set; }

            [JsonProperty("thumbs")]
            public IList<Thumb> Thumbs { get; set; }

            [JsonProperty("tags")]
            public IList<Tag> Tags { get; set; }
        }

        public class Video
        {
            [JsonProperty("video")]
            public Video2 video { get; set; }
        }

        public class SampleResponse1
        {
            [JsonProperty("videos")]
            public IList<Video> Videos { get; set; }

            [JsonProperty("count")]
            public int Count { get; set; }
        }
    }
}