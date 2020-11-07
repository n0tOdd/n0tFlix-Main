using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Crunchyroll
{
    public class StreamJsonResults
    {
        public class Metadata
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("series_id")]
            public string SeriesId { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("channel_id")]
            public object ChannelId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("episode_number")]
            public string EpisodeNumber { get; set; }

            [JsonProperty("display_episode_number")]
            public string DisplayEpisodeNumber { get; set; }

            [JsonProperty("is_mature")]
            public bool IsMature { get; set; }

            [JsonProperty("duration")]
            public int Duration { get; set; }
        }

        public class Thumbnail
        {
            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class Stream
        {
            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("audio_lang")]
            public string AudioLang { get; set; }

            [JsonProperty("hardsub_lang")]
            public string HardsubLang { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("resolution")]
            public string Resolution { get; set; }
        }

        public class AdBreak
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("offset")]
            public int Offset { get; set; }
        }

        public class Subtitle
        {
            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }
        }

        public class Preview
        {
            [JsonProperty("src")]
            public string Src { get; set; }
        }

        public class root
        {
            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("thumbnail")]
            public Thumbnail Thumbnail { get; set; }

            [JsonProperty("streams")]
            public IList<Stream> Streams { get; set; }

            [JsonProperty("ad_breaks")]
            public IList<AdBreak> AdBreaks { get; set; }

            [JsonProperty("subtitles")]
            public IList<Subtitle> Subtitles { get; set; }

            [JsonProperty("preview")]
            public Preview Preview { get; set; }
        }

        public static root GetFromJsonString(string json)
        {
            root rr = JsonConvert.DeserializeObject<root>(json);
            return rr;
        }
    }
}