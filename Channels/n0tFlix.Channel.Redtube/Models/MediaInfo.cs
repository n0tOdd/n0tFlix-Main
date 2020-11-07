using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Redtube.Models
{
    public class MediaInfo
    {
        public class MediaDefinition
        {
            [JsonProperty("defaultQuality")]
            public bool DefaultQuality { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("quality")]
            public string Quality { get; set; }

            [JsonProperty("videoUrl")]
            public string VideoUrl { get; set; }
        }

        public class root
        {
            [JsonProperty("mediaDefinitions")]
            public IList<MediaDefinition> MediaDefinitions { get; set; }
        }
    }
}