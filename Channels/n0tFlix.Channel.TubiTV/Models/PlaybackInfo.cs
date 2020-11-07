using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.TubiTV.Models
{
    public class PlaybackInfo
    {
        public class Rating
        {
            [JsonProperty("system")]
            public string System { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }
        }

        public class Images
        {
        }

        public class Awards
        {
            [JsonProperty("items")]
            public IList<object> Items { get; set; }
        }

        public class Manifest
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("duration")]
            public int Duration { get; set; }
        }

        public class VideoResource
        {
            [JsonProperty("manifest")]
            public Manifest Manifest { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class Subtitle
        {
            [JsonProperty("lang")]
            public string Lang { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class CreditCuepoints
        {
            [JsonProperty("postlude")]
            public double Postlude { get; set; }

            [JsonProperty("prelogue")]
            public int Prelogue { get; set; }
        }

        public class Monetization
        {
            [JsonProperty("cue_points")]
            public IList<int> CuePoints { get; set; }
        }

        public class root
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("tags")]
            public IList<string> Tags { get; set; }

            [JsonProperty("year")]
            public int Year { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("publisher_id")]
            public string PublisherId { get; set; }

            [JsonProperty("import_id")]
            public string ImportId { get; set; }

            [JsonProperty("ratings")]
            public IList<Rating> Ratings { get; set; }

            [JsonProperty("actors")]
            public IList<string> Actors { get; set; }

            [JsonProperty("directors")]
            public IList<string> Directors { get; set; }

            [JsonProperty("availability_duration")]
            public int AvailabilityDuration { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("has_subtitle")]
            public bool HasSubtitle { get; set; }

            [JsonProperty("imdb_id")]
            public string ImdbId { get; set; }

            [JsonProperty("partner_id")]
            public object PartnerId { get; set; }

            [JsonProperty("lang")]
            public string Lang { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("policy_match")]
            public bool PolicyMatch { get; set; }

            [JsonProperty("updated_at")]
            public string UpdatedAt { get; set; }

            [JsonProperty("awards")]
            public Awards Awards { get; set; }

            [JsonProperty("video_resources")]
            public IList<VideoResource> VideoResources { get; set; }

            [JsonProperty("posterarts")]
            public IList<string> Posterarts { get; set; }

            [JsonProperty("thumbnails")]
            public IList<string> Thumbnails { get; set; }

            [JsonProperty("hero_images")]
            public IList<string> HeroImages { get; set; }

            [JsonProperty("landscape_images")]
            public IList<object> LandscapeImages { get; set; }

            [JsonProperty("backgrounds")]
            public IList<string> Backgrounds { get; set; }

            [JsonProperty("gn_fields")]
            public object GnFields { get; set; }

            [JsonProperty("detailed_type")]
            public string DetailedType { get; set; }

            [JsonProperty("needs_login")]
            public bool NeedsLogin { get; set; }

            [JsonProperty("subtitles")]
            public IList<Subtitle> Subtitles { get; set; }

            [JsonProperty("has_trailer")]
            public bool HasTrailer { get; set; }

            [JsonProperty("canonical_id")]
            public string CanonicalId { get; set; }

            [JsonProperty("version_id")]
            public string VersionId { get; set; }

            [JsonProperty("valid_duration")]
            public int ValidDuration { get; set; }

            [JsonProperty("duration")]
            public int Duration { get; set; }

            [JsonProperty("credit_cuepoints")]
            public CreditCuepoints CreditCuepoints { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("monetization")]
            public Monetization Monetization { get; set; }

            [JsonProperty("availability_starts")]
            public string AvailabilityStarts { get; set; }

            [JsonProperty("availability_ends")]
            public object AvailabilityEnds { get; set; }

            [JsonProperty("trailers")]
            public IList<object> Trailers { get; set; }
        }
    }
}