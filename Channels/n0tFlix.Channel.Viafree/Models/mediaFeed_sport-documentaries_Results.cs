using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class mediaFeed_sport_documentaries_Results
    {
        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }

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

        public class Boxart
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Hero
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

            [JsonProperty("boxart")]
            public Boxart Boxart { get; set; }

            [JsonProperty("hero")]
            public Hero Hero { get; set; }
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

        public class LatestVideo
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("publishedAt")]
            public string PublishedAt { get; set; }

            [JsonProperty("episodeNumber")]
            public int? EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int? SeasonNumber { get; set; }
        }

        public class Series
        {
            [JsonProperty("numberOfSeasons")]
            public int NumberOfSeasons { get; set; }

            [JsonProperty("featureBoxPromo")]
            public string FeatureBoxPromo { get; set; }

            [JsonProperty("contextualLabel")]
            public string ContextualLabel { get; set; }

            [JsonProperty("latestVideo")]
            public LatestVideo LatestVideo { get; set; }
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

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Series2
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

        public class CustomDimensions
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("series")]
            public Series2 Series { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions CustomDimensions { get; set; }
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

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("subcategories")]
            public IList<Subcategory> Subcategories { get; set; }

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
            public object ChannelTabs { get; set; }
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

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/block/mediaFeed_sport-documentaries-{0}";

        /// <summary>
        ///returns a feed of sports documentaries, only needs the country code
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns selected sportsclip by publicpath or slug </returns>
        public static async System.Threading.Tasks.Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = null)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode));
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            root klar = JsonConvert.DeserializeObject<root>(json);
            return klar;
        }
    }
}