using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class mediaFeed_latest_episodes_Results
    {
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/block/mediaFeed_latest-episodes";

        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class StreamProgress
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Content
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }

            [JsonProperty("streamProgress")]
            public IList<StreamProgress> StreamProgress { get; set; }

            [JsonProperty("content")]
            public Content Content { get; set; }
        }

        public class MediaFeed
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("displayCount")]
            public int DisplayCount { get; set; }

            [JsonProperty("maxSize")]
            public int MaxSize { get; set; }
        }

        public class Synopsis
        {
            [JsonProperty("long")]
            public string Long { get; set; }

            [JsonProperty("short")]
            public string Short { get; set; }
        }

        public class Availability
        {
            [JsonProperty("start")]
            public string Start { get; set; }

            [JsonProperty("end")]
            public string End { get; set; }
        }

        public class PlayerPoster
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Landscape
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class SeasonImage
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Images
        {
            [JsonProperty("playerPoster")]
            public PlayerPoster PlayerPoster { get; set; }

            [JsonProperty("landscape")]
            public Landscape Landscape { get; set; }

            [JsonProperty("seasonImage")]
            public SeasonImage SeasonImage { get; set; }
        }

        public class Tag
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Channel
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }
        }

        public class Category
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Subcategory
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Episode
        {
            [JsonProperty("seriesId")]
            public string SeriesId { get; set; }

            [JsonProperty("seriesTitle")]
            public string SeriesTitle { get; set; }

            [JsonProperty("episodeNumber")]
            public int? EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int SeasonNumber { get; set; }

            [JsonProperty("extraMaterialsCount")]
            public int ExtraMaterialsCount { get; set; }

            [JsonProperty("episodeName")]
            public string EpisodeName { get; set; }
        }

        public class Subtitles
        {
            [JsonProperty("subtitlesWebvtt")]
            public string SubtitlesWebvtt { get; set; }

            [JsonProperty("samiPath")]
            public string SamiPath { get; set; }
        }

        public class Duration
        {
            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; }

            [JsonProperty("readable")]
            public string Readable { get; set; }
        }

        public class ImageLandscape
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class ImagePortrait
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class LoginRequired
        {
            [JsonProperty("upsellText")]
            public string UpsellText { get; set; }

            [JsonProperty("imageLandscape")]
            public ImageLandscape ImageLandscape { get; set; }

            [JsonProperty("imagePortrait")]
            public ImagePortrait ImagePortrait { get; set; }
        }

        public class Video
        {
            [JsonProperty("duration")]
            public Duration Duration { get; set; }

            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("loginRequired")]
            public LoginRequired LoginRequired { get; set; }
        }

        public class Broadcast
        {
            [JsonProperty("airAt")]
            public string AirAt { get; set; }

            [JsonProperty("endAt")]
            public string EndAt { get; set; }
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Series
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Season
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Play
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class StreamLink
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class AdInfo
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class CustomDimensions
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Tracking
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class StreamProgress2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("season")]
            public Season Season { get; set; }

            [JsonProperty("play")]
            public Play Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo AdInfo { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions CustomDimensions { get; set; }

            [JsonProperty("tracking")]
            public Tracking Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }

            [JsonProperty("streamProgress")]
            public StreamProgress2 StreamProgress { get; set; }
        }

        public class Program
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("synopsis")]
            public Synopsis Synopsis { get; set; }

            [JsonProperty("parentalRating")]
            public object ParentalRating { get; set; }

            [JsonProperty("availability")]
            public Availability Availability { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("flags")]
            public IList<string> Flags { get; set; }

            [JsonProperty("tags")]
            public IList<Tag> Tags { get; set; }

            [JsonProperty("channels")]
            public IList<Channel> Channels { get; set; }

            [JsonProperty("productKey")]
            public string ProductKey { get; set; }

            [JsonProperty("popularityScore")]
            public object PopularityScore { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("subcategories")]
            public IList<Subcategory> Subcategories { get; set; }

            [JsonProperty("episode")]
            public Episode Episode { get; set; }

            [JsonProperty("subtitles")]
            public Subtitles Subtitles { get; set; }

            [JsonProperty("video")]
            public Video Video { get; set; }

            [JsonProperty("broadcast")]
            public Broadcast Broadcast { get; set; }

            [JsonProperty("combinedTitle")]
            public string CombinedTitle { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }

            [JsonProperty("channelTabs")]
            public IList<string> ChannelTabs { get; set; }
        }

        public class root
        {
            [JsonProperty("componentName")]
            public string ComponentName { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("lazyBlock")]
            public bool LazyBlock { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [JsonProperty("mediaFeed")]
            public MediaFeed MediaFeed { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// Only country code needed here
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns all the latest episodes </returns>
        public static async Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = null)
        {
            HttpClient client = new HttpClient();

            string json = await client.GetStringAsync(String.Format(URL, CountryCode));

            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            root klar = JsonConvert.DeserializeObject<root>(json);
            return klar;
        }
    }
}