using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class ChannelResult
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/channel/viasat-4?device=web
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/channels?guid=DENNE <= husk her at denne må byttes med guid til channel du vil ha info ang
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/channels?guid={1}";

        public class Meta
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("image")]
            public string Image { get; set; }

            [JsonProperty("siteName")]
            public string SiteName { get; set; }

            [JsonProperty("siteUrl")]
            public string SiteUrl { get; set; }

            [JsonProperty("route")]
            public string Route { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("canonicalUrl")]
            public string CanonicalUrl { get; set; }
        }

        public class AbTesting
        {
            [JsonProperty("defaultVariant")]
            public string DefaultVariant { get; set; }

            [JsonProperty("availableVariants")]
            public IList<string> AvailableVariants { get; set; }
        }

        public class Self
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
        }

        public class Self2
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
        }

        public class Image
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Logo
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class LogoDark
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Bug
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class BugDark
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
            [JsonProperty("image")]
            public Image Image { get; set; }

            [JsonProperty("logo")]
            public Logo Logo { get; set; }

            [JsonProperty("logoDark")]
            public LogoDark LogoDark { get; set; }

            [JsonProperty("bug")]
            public Bug Bug { get; set; }

            [JsonProperty("bugDark")]
            public BugDark BugDark { get; set; }
        }

        public class Channel2
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

        public class Links3
        {
            [JsonProperty("channel")]
            public Channel2 Channel { get; set; }
        }

        public class Channel
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("_links")]
            public Links3 Links { get; set; }
        }

        public class MediaFeed
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("maxSize")]
            public int MaxSize { get; set; }

            [JsonProperty("displayCount")]
            public int DisplayCount { get; set; }
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

        public class Images2
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

        public class Channel3
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

        public class Self3
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

        public class Links4
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }

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
            public Images2 Images { get; set; }

            [JsonProperty("flags")]
            public IList<string> Flags { get; set; }

            [JsonProperty("tags")]
            public IList<Tag> Tags { get; set; }

            [JsonProperty("channels")]
            public IList<Channel3> Channels { get; set; }

            [JsonProperty("productKey")]
            public string ProductKey { get; set; }

            [JsonProperty("popularityScore")]
            public int? PopularityScore { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("subcategories")]
            public IList<Subcategory> Subcategories { get; set; }

            [JsonProperty("combinedTitle")]
            public string CombinedTitle { get; set; }

            [JsonProperty("_links")]
            public Links4 Links { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }
        }

        public class ViafreeBlock
        {
            [JsonProperty("componentName")]
            public string ComponentName { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("lazyBlock")]
            public bool LazyBlock { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [JsonProperty("channel")]
            public Channel Channel { get; set; }

            [JsonProperty("mediaFeed")]
            public MediaFeed MediaFeed { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("viafreeBlocks")]
            public IList<ViafreeBlock> ViafreeBlocks { get; set; }
        }

        public class root
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("meta")]
            public Meta Meta { get; set; }

            [JsonProperty("abTesting")]
            public IList<AbTesting> AbTesting { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// If not using public path then change path to the guid of the channel you want
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static async Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = "/kanal/viasat-4")
        {
            WebClient client = new WebClient();
            Console.WriteLine("Collecting channel item results");
            if (UsePulicPath)
            {
                string json = await client.DownloadStringTaskAsync(String.Format("https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path{1} ", CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
                string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
        }
    }
}