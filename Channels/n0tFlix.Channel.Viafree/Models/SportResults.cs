using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    /// <summary>
    /// Gives you the sport main page with aall its glory
    /// </summary>
    public static class SportResults
    {
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/page/sport?lazy=false&testVariant=defa";

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

        public class Content
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

            [JsonProperty("content")]
            public Content Content { get; set; }
        }

        public class Advertisement
        {
            [JsonProperty("adType")]
            public string AdType { get; set; }
        }

        public class Self3
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }
        }

        public class AdRequestKeyValues
        {
            [JsonProperty("testPreroll")]
            public string TestPreroll { get; set; }
        }

        public class Freewheel
        {
            [JsonProperty("serverUrl")]
            public string ServerUrl { get; set; }

            [JsonProperty("networkId")]
            public string NetworkId { get; set; }

            [JsonProperty("siteSectionNetworkId")]
            public string SiteSectionNetworkId { get; set; }

            [JsonProperty("siteSectionId")]
            public string SiteSectionId { get; set; }

            [JsonProperty("profileId")]
            public string ProfileId { get; set; }

            [JsonProperty("adRequestKeyValues")]
            public AdRequestKeyValues AdRequestKeyValues { get; set; }
        }

        public class Embedded3
        {
            [JsonProperty("freewheel")]
            public Freewheel Freewheel { get; set; }
        }

        public class AdInfo
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("_links")]
            public Links3 Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded3 Embedded { get; set; }
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

        public class Self4
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
            public Self4 Self { get; set; }

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
            public Links4 Links { get; set; }
        }

        public class SportClipPoster
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class PlayerPoster2
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
            [JsonProperty("sportClipPoster")]
            public SportClipPoster SportClipPoster { get; set; }

            [JsonProperty("playerPoster")]
            public PlayerPoster2 PlayerPoster { get; set; }
        }

        public class Synopsis2
        {
            [JsonProperty("long")]
            public string Long { get; set; }

            [JsonProperty("short")]
            public string Short { get; set; }
        }

        public class Availability2
        {
            [JsonProperty("start")]
            public string Start { get; set; }

            [JsonProperty("end")]
            public object End { get; set; }
        }

        public class Sport
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }
        }

        public class Competition
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }
        }

        public class SportTags
        {
            [JsonProperty("programType")]
            public object ProgramType { get; set; }

            [JsonProperty("sport")]
            public Sport Sport { get; set; }

            [JsonProperty("competition")]
            public Competition Competition { get; set; }

            [JsonProperty("contents")]
            public IList<string> Contents { get; set; }

            [JsonProperty("events")]
            public IList<object> Events { get; set; }
        }

        public class SportClip2
        {
            [JsonProperty("preamble")]
            public object Preamble { get; set; }

            [JsonProperty("sportTags")]
            public SportTags SportTags { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public class Duration
        {
            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; }

            [JsonProperty("readable")]
            public string Readable { get; set; }
        }

        public class Video
        {
            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("duration")]
            public Duration Duration { get; set; }
        }

        public class Self5
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
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
        }

        public class AdInfo2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class CustomDimensions2
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

        public class Links5
        {
            [JsonProperty("self")]
            public Self5 Self { get; set; }

            [JsonProperty("play")]
            public Play Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo2 AdInfo { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions2 CustomDimensions { get; set; }

            [JsonProperty("tracking")]
            public Tracking Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }
        }

        public class SportClip
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("images")]
            public Images2 Images { get; set; }

            [JsonProperty("synopsis")]
            public Synopsis2 Synopsis { get; set; }

            [JsonProperty("availability")]
            public Availability2 Availability { get; set; }

            [JsonProperty("sportClip")]
            public SportClip2 sportClip { get; set; }

            [JsonProperty("video")]
            public Video Video { get; set; }

            [JsonProperty("_links")]
            public Links5 Links { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("adInfo")]
            public AdInfo AdInfo { get; set; }

            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }

            [JsonProperty("channelTabs")]
            public object ChannelTabs { get; set; }

            [JsonProperty("sportClips")]
            public IList<SportClip> SportClips { get; set; }
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

        public class SportBlock
        {
            [JsonProperty("sportSlug")]
            public string SportSlug { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
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

            [JsonProperty("advertisement")]
            public Advertisement Advertisement { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }

            [JsonProperty("mediaFeed")]
            public MediaFeed MediaFeed { get; set; }

            [JsonProperty("sportBlock")]
            public SportBlock SportBlock { get; set; }
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
        /// Gives you the main sport page in all its glory, only countrycode needed here
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