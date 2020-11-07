using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Redtube.Models
{
    public class Info
    {
        public class Category
        {
            public string category { get; set; }
        }

        public class Tag2
        {
            public string tag_name { get; set; }
        }

        public class Tag
        {
            public Tag2 tag { get; set; }
        }

        public class Star2
        {
            public string star_name { get; set; }
            public string star_thumb { get; set; }
            public string star_url { get; set; }
        }

        public class Star
        {
            public Star2 star { get; set; }
        }

        public class Video
        {
            public string default_thumb { get; set; }
            public string duration { get; set; }
            public string publish_date { get; set; }
            public string rating { get; set; }
            public string ratings { get; set; }
            public List<string> tags { get; set; }
            public string thumb { get; set; }
            public List<Thumb> thumbs { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string video_id { get; set; }
            public string views { get; set; }

            public Video2 video { get; set; }
        }

        public class Active
        {
            public string video_id { get; set; }
            public bool is_active { get; set; }
        }

        public class Thumb
        {
            public int height { get; set; }
            public string size { get; set; }
            public string src { get; set; }
            public int width { get; set; }
        }

        public class Video2
        {
            public string default_thumb { get; set; }
            public string duration { get; set; }
            public string publish_date { get; set; }
            public string rating { get; set; }
            public string ratings { get; set; }
            public List<Tag> tags { get; set; }
            public string thumb { get; set; }
            public List<Thumb> thumbs { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string video_id { get; set; }
            public int views { get; set; }
        }

        public class RootObject
        {
            public int count { get; set; }
            public List<Video> videos { get; set; }
            public List<Category> categories { get; set; }
            public List<Tag> tags { get; set; }
            public List<Star> stars { get; set; }
            public Video video { get; set; }
            public Active active { get; set; }
        }
    }
}