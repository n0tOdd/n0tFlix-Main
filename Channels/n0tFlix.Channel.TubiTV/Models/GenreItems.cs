using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.TubiTV.Models
{
    public class GenreItems
    {
        public string id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string Language { get; set; }

        public List<string> tags { get; set; }

        public List<string> Directors { get; set; }
        public List<string> Actors { get; set; }

        public List<string> PosterArt { get; set; }
        public List<string> Thumbnail { get; set; }

        public GenreItems(string id, string type, string title, string description, string languege, List<string> tags, List<string> Directors, List<string> Actors, List<string> Posterart, List<string> thumbnails)
        {
            this.id = id;
            this.type = type;
            this.title = title;
            this.description = description;
            this.Language = languege;
            this.tags = tags;
            this.Directors = Directors;
            this.Actors = Actors;
            this.PosterArt = PosterArt;
            this.Thumbnail = thumbnails;
        }
    }
}