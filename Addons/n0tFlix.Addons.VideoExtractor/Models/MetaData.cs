using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Models
{
    public class MetaData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Genres { get; set; }
        public string PlayTime { get; set; }
        public string OrginalURL { get; set; }
        public string DownloadURL { get; set; }
    }
}