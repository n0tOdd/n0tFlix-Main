using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Pornhub.Models
{
    public class SearchResult
    {
        public class Thumb
        {
            [JsonProperty("size")]
            public string Size { get; set; }

            [JsonProperty("width")]
            public string Width { get; set; }

            [JsonProperty("height")]
            public string Height { get; set; }

            [JsonProperty("src")]
            public string Src { get; set; }
        }

        public class Tag
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }
        }

        public class Category
        {
            [JsonProperty("category")]
            public string category { get; set; }
        }

        public class Video
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

            [JsonProperty("pornstars")]
            public IList<object> Pornstars { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("segment")]
            public string Segment { get; set; }
        }

        internal class root
        {
            [JsonProperty("videos")]
            public IList<Video> Videos { get; set; }
        }
    }
}