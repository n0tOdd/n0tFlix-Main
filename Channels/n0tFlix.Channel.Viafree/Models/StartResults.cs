using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class StartResults
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/start

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/page/start";

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

            [JsonProperty("templated")]
            public bool? Templated { get; set; }

            [JsonProperty("userContent")]
            public bool? UserContent { get; set; }
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

        public class Theme
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

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("content")]
            public Content Content { get; set; }

            [JsonProperty("streamProgress")]
            public IList<StreamProgress> StreamProgress { get; set; }

            [JsonProperty("theme")]
            public Theme Theme { get; set; }
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

        public class Hero
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

        public class Boxart
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class HeroVideo
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class HeroVideoPoster
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

            [JsonProperty("hero")]
            public Hero Hero { get; set; }

            [JsonProperty("seasonImage")]
            public SeasonImage SeasonImage { get; set; }

            [JsonProperty("boxart")]
            public Boxart Boxart { get; set; }

            [JsonProperty("heroVideo")]
            public HeroVideo HeroVideo { get; set; }

            [JsonProperty("heroVideoPoster")]
            public HeroVideoPoster HeroVideoPoster { get; set; }
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

        public class Links3
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }

            [JsonProperty("series")]
            public Series2 Series { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions CustomDimensions { get; set; }

            [JsonProperty("season")]
            public Season Season { get; set; }

            [JsonProperty("play")]
            public Play Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo AdInfo { get; set; }

            [JsonProperty("tracking")]
            public Tracking Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }

            [JsonProperty("streamProgress")]
            public StreamProgress2 StreamProgress { get; set; }
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

            [JsonProperty("episodeName")]
            public string EpisodeName { get; set; }

            [JsonProperty("extraMaterialsCount")]
            public int ExtraMaterialsCount { get; set; }
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
            public Links3 Links { get; set; }

            [JsonProperty("episode")]
            public Episode Episode { get; set; }

            [JsonProperty("subtitles")]
            public Subtitles Subtitles { get; set; }

            [JsonProperty("video")]
            public Video Video { get; set; }

            [JsonProperty("broadcast")]
            public Broadcast Broadcast { get; set; }
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

        public class Images2
        {
            [JsonProperty("image")]
            public Image Image { get; set; }
        }

        public class Category3
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

        public class Links4
        {
            [JsonProperty("category")]
            public Category3 Category { get; set; }
        }

        public class Category2
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

            [JsonProperty("images")]
            public Images2 Images { get; set; }

            [JsonProperty("_links")]
            public Links4 Links { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }

            [JsonProperty("channelTabs")]
            public IList<string> ChannelTabs { get; set; }

            [JsonProperty("categories")]
            public IList<Category2> Categories { get; set; }
        }

        public class UserContent
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("maxSize")]
            public int MaxSize { get; set; }
        }

        public class CategoryList
        {
            [JsonProperty("title")]
            public string Title { get; set; }
        }

        public class LogoLight
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

        public class Images3
        {
            [JsonProperty("logoLight")]
            public LogoLight LogoLight { get; set; }

            [JsonProperty("logoDark")]
            public LogoDark LogoDark { get; set; }
        }

        public class Theme3
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

        public class Links5
        {
            [JsonProperty("theme")]
            public Theme3 Theme { get; set; }
        }

        public class Theme2
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("images")]
            public Images3 Images { get; set; }

            [JsonProperty("_links")]
            public Links5 Links { get; set; }

            [JsonProperty("image")]
            public object Image { get; set; }

            [JsonProperty("logoLight")]
            public string LogoLight { get; set; }

            [JsonProperty("logoDark")]
            public string LogoDark { get; set; }
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

            [JsonProperty("mediaFeed")]
            public MediaFeed MediaFeed { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }

            [JsonProperty("userContent")]
            public UserContent UserContent { get; set; }

            [JsonProperty("categoryList")]
            public CategoryList CategoryList { get; set; }

            [JsonProperty("theme")]
            public Theme2 Theme { get; set; }
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
        /// Returns the start page wiith all the shows from there, no publicpath and no path variaable needed here
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns the start page in all its glory </returns>
        public static async System.Threading.Tasks.Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = null)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            string publicPathURL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path{1}";
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